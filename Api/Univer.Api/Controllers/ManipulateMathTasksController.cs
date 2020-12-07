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
            if (matrixMultiplyRequest.columns_1 != matrixMultiplyRequest.rows_2)
            {
                return new JsonResult( new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = "Multiplication is not possible! The number of columns in the first matrix is ​​not equal to the number of rows in the second matrix." } );
            }

            try
            {
                long result = await _mathService.MatrixMultiply(matrixMultiplyRequest: matrixMultiplyRequest, cancellationToken: cancellationToken);

                return new JsonResult(new ResponseBase<long> { Data = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new ResponseBase<string> {Status = ResponeStatusCodes.RequestIsCanceled, Data = ex.Message });
            }

        }


    }
}
