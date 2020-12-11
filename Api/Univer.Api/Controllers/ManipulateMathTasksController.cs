using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Univer.BLL.Services;
using Univer.DAL;
using Univer.DAL.Models;
using Univer.DAL.Models.Math;

namespace Univer.Api.Controllers
{
    [Route("api")]
    [Authorize]
    public class ManipulateMathTasksController: ControllerBase
    {
        private readonly IMathService _mathService;

        public ManipulateMathTasksController(IMathService mathService)
        {
            this._mathService = mathService;
        }


        [HttpPost("math-task")]
        public async Task<ActionResult> MatrixMultiply([FromBody] MatrixMultiplyRequest matrixMultiplyRequest, CancellationToken cancellationToken) 
        {
            object result = await _mathService.MatrixMultiply(matrixMultiplyRequest: matrixMultiplyRequest, cancellationToken: cancellationToken);

            return new JsonResult(result);
        }


    }
}
