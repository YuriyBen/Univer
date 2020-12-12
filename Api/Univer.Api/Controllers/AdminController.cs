using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.BLL.Services;

namespace Univer.Api.Controllers
{
    [Route("api")]
    [Authorize(Roles = "Admin")]
    public class AdminController: ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            this._adminService = adminService;
        }

        [HttpGet("admin")]
        public ActionResult GetHistory()
        {
            var result = _adminService.GetUsersHistories();
            return new JsonResult(result);
        }

    }
}
