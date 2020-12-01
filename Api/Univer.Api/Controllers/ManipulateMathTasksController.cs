using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Univer.BLL.Services;
using Univer.DAL;
using Univer.DAL.Models.Math;

namespace Univer.Api.Controllers
{
    [Route("api")]
    public class ManipulateMathTasksController: ControllerBase
    {
        private readonly IMathService _mathService;
        private readonly AppDbContext _context;

        public ManipulateMathTasksController(IMathService mathService, AppDbContext context)
        {
            this._mathService = mathService;
            this._context = context;
        }


        //[HttpPost("register")]
        //public async Task<ActionResult> MatrixMultiply([FromBody] MatrixMultiplyRequest matrixMultiplyRequest)
        //{
        //    var result = _mathService.MatrixMultiply();

        //}


    }
}
