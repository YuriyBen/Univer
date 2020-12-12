using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univer.DAL;
using Univer.DAL.Entities;
using Univer.DAL.Models;
using Univer.DAL.Models.Admin;

namespace Univer.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            this._context = context;
        }

        public object GetUsersHistories()
        {
            try
            {
                List<UserPublicData> usersData = this._context.UsersPublicData.ToList();

                List<AdminHistory> adminCollection = new List<AdminHistory>();

                foreach (UserPublicData user in usersData)
                {
                    List<History> usersHistory =  this._context.History.Where(x=>x.UserPublicDataId == user.Id).ToList();
                    foreach (History history in usersHistory)
                    {

                        adminCollection.Add(new AdminHistory
                        {
                            Username = user.UserName,
                            MatrixSizes = history.MatrixSizes,
                            Date = history.Date.ToShortDateString(),
                            Result = history.Result,
                            IsCanceled = history.IsCanceled,
                            IsCurrentlyExecuted = history.IsCurrentlyExecuted
                        });
                    }
                }
                return new ResponseBase<List<AdminHistory>> { Data = adminCollection };

            }
            catch (Exception ex)
            {
                return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Oops...{ex.Message}" };
            }


        }
    }
}
