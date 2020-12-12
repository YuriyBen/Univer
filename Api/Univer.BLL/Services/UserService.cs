using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Univer.DAL.Entities;
using Univer.DAL.Models;
using Univer.DAL.Helpers;
using Univer.DAL;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Univer.DAL.Models.Account;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Univer.DAL.Helpers.Settings;

namespace Univer.BLL.Services
{
    public class UserService : IUserService
    {
        private const int AccessTokenExpiresInHours = 4;
        private const int RefreshTokenExpiresInHours = 7;
        private const double SecretKeyToVerifyPhoneValidInMinutes = 5;
        private readonly AppDbContext _context;
        private readonly TwilioPhoneVerification _phoneVerificationSection;
        private readonly AppSettings _appSettings;

        public UserService(AppDbContext context, IOptions<AppSettings> appSettings, IOptions<TwilioPhoneVerification> phoneVerificationSection)
        {
            this._context = context;
            this._phoneVerificationSection = phoneVerificationSection.Value;
            this._appSettings = appSettings.Value;
        }

        
        public async Task<object> Register(Register register)
        {
            try
            {
                register.Phone = register.Phone.Trim()[0] == '+' ? register.Phone.Trim() : $"+{register.Phone.Trim()}";

                User user = this._context.Users.FirstOrDefault(user => user.Phone == register.Phone);

                if (user != null)
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.UserAlreayExists, Data = ResponseMessages.UserAlreayExists };
                }


                user = new User { Phone = register.Phone, PasswordHash = register.Password.HashPassword() };


                UserPublicData userPublicData = new UserPublicData { User = user, UserName = $"{register.FirstName} {register.LastName}" };

                await this._context.Users.AddAsync(user);
                await this._context.UsersPublicData.AddAsync(userPublicData);

                await this._context.SaveChangesAsync();

                RegisterResponse userToReturn = new RegisterResponse { Id = user.Id, Phone = user.Phone, UserName = userPublicData.UserName, SecretKey = await this.SendMessageViaPhone(register.Phone) };

                return new ResponseBase<UserDTO> { Data = userToReturn };

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

            }
        }

        public async Task<object> Verification(PhoneVerificationRequest verificationRequest)
        {
            try
            {
                User user = await this._context.Users.FirstOrDefaultAsync(user => user.Id == verificationRequest.UserId);

                if (user.Role != RoleType.Unverified)
                {
                    return new ResponseBase<string> { Data = ResponseMessages.VerifiedUser };
                }

                UserPublicData userData = await this._context.UsersPublicData.FirstOrDefaultAsync(user => user.UserId == verificationRequest.UserId);

                if (userData.SecretKey == verificationRequest.SecretKey && DateTime.Now < userData.SecretKeyValidTo )
                {
                    user.Role = RoleType.User;
                    await this._context.SaveChangesAsync();

                    return new ResponseBase<string> { Data = ResponseMessages.VerifiedUser };

                }
                else
                {
                    userData.UnsuccessfullyVerificationAttempts++;
                    if (userData.UnsuccessfullyVerificationAttempts == 3)
                    {
                        userData.SecretKey = await this.SendMessageViaPhone(userData.User.Phone);
                        userData.SecretKeyValidTo = DateTime.Now.AddMinutes(SecretKeyToVerifyPhoneValidInMinutes);
                        userData.UnsuccessfullyVerificationAttempts = 0;

                        await this._context.SaveChangesAsync();

                        return new ResponseBase<string> { Status = ResponeStatusCodes.UnverifiedUser, Data = ResponseMessages.RepeatedVerification };

                    }
                    await this._context.SaveChangesAsync();
                    return new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = ResponseMessages.BadVerificationSecretKey };


                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Oops... {ex.Message}" };
            }


        }
        private async Task<long> SendMessageViaPhone(string phoneNumber)
        {
            Random random = new Random();
            long secretKey = random.Next(1000, 9999);

            await Task.Run(() =>
            {
                TwilioClient.Init(this._phoneVerificationSection.AccountSid, this._phoneVerificationSection.AuthToken);

                var message = MessageResource.Create(
                    to: new PhoneNumber(phoneNumber),
                    from: new PhoneNumber(this._phoneVerificationSection.PhoneNumberToSendSMS),
                    body: $"\nDo not show it to anyone!\nYour secret key to verify yourself: {secretKey}");


            });

            return secretKey;
        }

        public async Task<object> Login(Login login)
        {
            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(user => user.Phone == login.Phone);

                if (user == null || !login.Password.CheckPasswordWithHash(user.PasswordHash).Verified)
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.InvalidLoginOrPassword, Data = ResponseMessages.InvalidLoginOrPassword };
                }




                if (user.Role == RoleType.Unverified)
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.UnverifiedUser, Data = ResponseMessages.UnverifiedUser };
                }

                user.UserPublicData = _context.UsersPublicData.FirstOrDefault(userData => userData.UserId == user.Id);

                (string accessToken, string refreshToken) = this.GenerateJWT_Tokens(userId: user.Id, userRole: user.Role);

                return new ResponseBase<LoginResponse> { Data = new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, User = new UserDTO { Id = user.Id, Phone = user.Phone, UserName = user.UserPublicData.UserName } } };
            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

            }
        }

        public object RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {

            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken decodedTokenData = handler.ReadToken(refreshTokenRequest.RefreshToken) as JwtSecurityToken;

                int userIdFromToken = Int32.Parse(decodedTokenData.Claims.FirstOrDefault(x => x.Type == "unique_name").Value);

                string userRole = decodedTokenData.Claims.FirstOrDefault(x => x.Type == "role").Value;


                DateTime validTo = decodedTokenData.ValidTo;
                DateTime now = DateTime.Now;

                if (decodedTokenData.ValidTo <= DateTime.Now)
                {
                    (string accessToken, string refreshToken) = this.GenerateJWT_Tokens(userId: userIdFromToken, userRole: userRole);
                    return new ResponseBase<RefreshTokenResponse> { Data = new RefreshTokenResponse { AccessToken = accessToken, RefreshToken = refreshToken } };
                }
                else
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.TokenIsValid, Data = ResponseMessages.TokenIsValid };
                }

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

            }
        }

        private (string AccessToken, string RefreshToken) GenerateJWT_Tokens(int userId, string userRole)
        {
            string accessToken = JWT_Helper.GenerateJWT(secretKey: this._appSettings.JWT_SecretKey, userId: userId, role: userRole, expiresInHours: AccessTokenExpiresInHours);
            string refreshToken = JWT_Helper.GenerateJWT(secretKey: this._appSettings.JWT_SecretKey, userId: userId, role: userRole, expiresInHours: RefreshTokenExpiresInHours);

            return (AccessToken: accessToken, RefreshToken: refreshToken);
        }

        public object GetMyHistory(SimpleIdRequest simpleIdRequest)
        {
            try
            {
                IEnumerable<HistoryDTO> myHistory = _context.UsersPublicData.Include(x => x.History)
                        .Where(x => x.UserId == simpleIdRequest.Id)
                        .SelectMany(x => x.History
                        .Select(history =>
                           new HistoryDTO
                            {
                                Id = history.Id,
                                Date = history.Date.ToShortDateString(),
                                MatrixSizes = history.MatrixSizes,
                                Result = history.Result,
                                IsCurrentlyExecuted = history.IsCurrentlyExecuted,
                                IsCanceled = history.IsCanceled
                            }));

                return new ResponseBase<IEnumerable<HistoryDTO>> { Data = myHistory };
            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

            }

        }

    }
}
