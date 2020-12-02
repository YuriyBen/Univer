using System;
using System.Threading.Tasks;
using Univer.DAL.Entities;
using Univer.DAL.Models;
using Univer.DAL.Models.Account;

namespace Univer.BLL.Services
{
    public interface IUserService
    {
        Task<object> Register(Register register);
        Task<object> Login(Login login);
        object GetMyHistory(SimpleIdRequest simpleIdRequest);
    }
}