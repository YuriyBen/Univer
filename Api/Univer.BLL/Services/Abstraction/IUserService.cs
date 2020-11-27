using System;
using System.Threading.Tasks;
using Univer.DAL.Entities;
using Univer.DAL.Models;

namespace Univer.BLL.Services
{
    public interface IUserService
    {
        Task<object> Register(Register register);
    }
}