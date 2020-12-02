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

namespace Univer.BLL.Services
{
    public class UserService : IUserService
    {
        private const int AccessTokenExpiresInHours = 2;
        private const int RefreshTokenExpiresInHours = 5;
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;

        public UserService(AppDbContext context, IOptions<AppSettings> appSettings)
        {
            this._context = context;
            this._appSettings = appSettings.Value;
        }

        public async Task<object> Register(Register register)
        {
            try
            {
                User user = this._context.Users.FirstOrDefault(user => user.Email == register.Email);

                if (user != null)
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.UserAlreayExists, Data = $"Ooops. {ErrorMessages.UserAlreayExists}" };
                }

                user = new User { Email = register.Email, PasswordHash = register.Password.HashPassword() };


                UserPublicData userPublicData = new UserPublicData { User = user, UserName = $"{register.FirstName} {register.LastName}" };

                await this._context.Users.AddAsync(user);
                await this._context.UsersPublicData.AddAsync(userPublicData);

                await this._context.SaveChangesAsync();

                UserDTO userToReturn = new UserDTO { Id = user.Id, Email = user.Email, UserName = userPublicData.UserName };

                return new ResponseBase<UserDTO> { Data = userToReturn };

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = $"Ooops. {ex.Message}" };

            }
        }

        public async Task<object> Login(Login login)
        {

            User user = await _context.Users.FirstOrDefaultAsync(user => user.Email == login.Email);

            if (user == null || !login.Password.CheckPasswordWithHash(user.PasswordHash).Verified)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.InvalidLoginOrPassword, Data = ErrorMessages.InvalidLoginOrPassword };
            }

            user.UserPublicData = _context.UsersPublicData.FirstOrDefault(userData => userData.UserId == user.Id);

            string accessToken = JWT_Helper.GenerateJWT(secretKey: this._appSettings.JWT_SecretKey, userId: user.Id, expiresInHours: AccessTokenExpiresInHours);
            string refreshToken = JWT_Helper.GenerateJWT(secretKey: this._appSettings.JWT_SecretKey, userId: user.Id, expiresInHours: RefreshTokenExpiresInHours);

            return new ResponseBase<LoginResponse> { Data = new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken, User = new UserDTO { Id = user.Id, Email = user.Email, UserName = user.UserPublicData.UserName } } };
        }

        public object GetMyHistory(SimpleIdRequest simpleIdRequest)
        {

            IEnumerable<HistoryDTO> myHistory = _context.UsersPublicData.Include(x => x.History)
                    .Where(x => x.UserId == simpleIdRequest.Id)
                    .SelectMany(x => x.History
                    .Select( history => 
                        new HistoryDTO 
                        { 
                            Id = history.Id,
                            Date = history.Date,
                            MatrixSizes = history.MatrixSizes,
                            Result = history.Result 
                        } ));


            return new ResponseBase<IEnumerable<HistoryDTO>> { Data = myHistory};

        }
    }
}
