using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.BLL.Services;
using Univer.DAL;
using Univer.DAL.Entities;
using Univer.DAL.Helpers;
using Univer.DAL.Models;

namespace Univer.Api.Controllers
{
    [Route("api")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;

        public AccountController(IUserService userService, AppDbContext context)
        {
            this._userService = userService;
            this._context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Register registerRequest)
        {
            var result = await _userService.Register(registerRequest);

            return new JsonResult( result );
        }

    }
}
