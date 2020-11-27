using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Univer.DAL.Entities;
using Univer.DAL.Models;
using Univer.DAL.Helpers;
using Univer.DAL;
using System.Linq;

namespace Univer.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<object> Register(Register register)
        {
            try
            {
                User user = this._context.Users.FirstOrDefault(user => user.Email == register.Email);

                if( user != null)
                {
                    return new ResponseBase<string> { Status = ResponeStatusCodes.InvalidLoginOrPassword, Data = $"Ooops. {ErrorMessages.InvalidLoginOrPassword}" };
                }

                user = new User { Email = register.Email, PasswordHash = register.Password.HashPassword() };
                

                UserPublicData userPublicData = new UserPublicData { User = user, UserName = $"{register.FirstName} {register.LastName}" };

                await this._context.Users.AddAsync(user);
                await this._context.UsersPublicData.AddAsync(userPublicData);

                await this._context.SaveChangesAsync();

                UserDTO userToReturn =  new UserDTO { Id = user.Id, Email = user.Email, UserName = userPublicData.UserName };

                return new ResponseBase<UserDTO> {  Data = userToReturn};

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = $"Ooops. {ex.Message}" };

            }
        }

        
    }
}
