using System.Collections.Generic;
using System.Threading.Tasks;
using Univer.DAL.Models;
using Univer.DAL.Models.Admin;

namespace Univer.BLL.Services
{
    public interface IAdminService
    {
        object GetUsersHistories();
    }
}