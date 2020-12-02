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

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Register registerRequest)
        {
            var result = await _userService.Register(register: registerRequest);

            return new JsonResult(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login loginRequest)
        {

            var result = await _userService.Login(login: loginRequest);
            return new JsonResult(result);
        }

        [HttpGet("account/history")]
        public async Task<ActionResult> GetHistory()
        {
            return new JsonResult(32);
        }

    }
}
