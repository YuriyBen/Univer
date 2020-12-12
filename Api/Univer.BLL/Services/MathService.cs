using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Univer.DAL;
using Univer.DAL.Entities;
using Univer.DAL.Models;
using Univer.DAL.Models.Math;

namespace Univer.BLL.Services
{
    public class MathService : IMathService
    {
		private const int AmountOfSimulateniouslyExecutedTasks = 2;
        private readonly AppDbContext _context;

        public MathService(AppDbContext context)
        {
            this._context = context;
        }

		private async Task PreviousStateDueToCanceledRequest(History history, CancellationToken cancellationToken)
        {

			history.IsCanceled = true;
			history.IsCurrentlyExecuted = false;

			await this.ModifyHistoryInDb(historyToModify: history);

			await this._context.SaveChangesAsync();

			cancellationToken.ThrowIfCancellationRequested();

		}
		private int[,] ConvertJsonToArray(string array)
        {
			return Newtonsoft.Json.JsonConvert.DeserializeObject<int[,]>(array);
		}
		public async Task<object> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest, CancellationToken cancellationToken)
        {
			MatrixMultiplyDTO matrixMultiplyDTO = new MatrixMultiplyDTO
			{
				UserId = matrixMultiplyRequest.UserId,
				Matrix1 = this.ConvertJsonToArray(matrixMultiplyRequest.Matrix1),
				Matrix2 = this.ConvertJsonToArray(matrixMultiplyRequest.Matrix2)
			};

			int rows_1 = matrixMultiplyDTO.Matrix1.GetUpperBound(0) + 1;
			int columns_1 = matrixMultiplyDTO.Matrix1.GetUpperBound(1) + 1;
			int rows_2 = matrixMultiplyDTO.Matrix2.GetUpperBound(0) + 1;
			int columns_2 = matrixMultiplyDTO.Matrix2.GetUpperBound(1) + 1;

			if (columns_1 != rows_2)
			{
				return new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = ResponseMessages.MatrixSizesEror };
			}

			try
			{
				int userPublicDataId = _context.UsersPublicData.FirstOrDefault(u => u.UserId == matrixMultiplyDTO.UserId).Id;

				int amountOfSimultaneouslyExecutedTasks = await this._context.History.Where(item => item.UserPublicDataId == userPublicDataId && item.IsCurrentlyExecuted).CountAsync();

				if (amountOfSimultaneouslyExecutedTasks + 1 /*+1 means +1 thread for current task*/ > AmountOfSimulateniouslyExecutedTasks)
				{
					return new ResponseBase<string> { Status = ResponeStatusCodes.LimitOfExecutableTasks, Data = ResponseMessages.LimitOfExecutableTasks };
				}

				string formattedMatrixSizes = this.FormatMatrixSize(rows_1, columns_1, rows_2, columns_2);
				History history = await this.AddMathResultToDb(result: 0, userPublicDataId: userPublicDataId, formattedMatrixSize: formattedMatrixSizes, isBeingExecuted: true);

				var matrix3 = new int[rows_1, columns_2];

				for (var i = 0; i < rows_1; i++)
				{
					for (var j = 0; j < columns_2; j++)
					{
						matrix3[i, j] = 0;
						if (cancellationToken.IsCancellationRequested)
						{
							await this.PreviousStateDueToCanceledRequest(history: history, cancellationToken: cancellationToken);
						}

						for (var k = 0; k < columns_1; k++)
						{
							matrix3[i, j] += matrixMultiplyDTO.Matrix1[i, k] * matrixMultiplyDTO.Matrix2[k, j];
							if (cancellationToken.IsCancellationRequested)
							{
								await this.PreviousStateDueToCanceledRequest(history: history, cancellationToken: cancellationToken);
							}
						}
					}
				}

				long sum = 0;

				for (var i = 0; i < rows_1; i++)
				{
					for (var j = 0; j < columns_2; j++)
					{
						sum += matrix3[i, j];
						if (cancellationToken.IsCancellationRequested)
						{
							await this.PreviousStateDueToCanceledRequest(history: history, cancellationToken: cancellationToken);
						}

					}
				}

				history.IsCurrentlyExecuted = false;
				history.Result = sum;
				await this.ModifyHistoryInDb(historyToModify: history);

				return new ResponseBase<string> { Data = $"Result is equal to {sum}" };

			}
			catch (Exception ex)
			{
				return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

			}

		}

        private async Task<History> AddMathResultToDb(int result, int userPublicDataId, string formattedMatrixSize, bool isBeingExecuted = false)
        {
			History history =  this._context.History.Add(new History { Date = DateTime.Now, UserPublicDataId = userPublicDataId, Result = result, MatrixSizes = formattedMatrixSize, IsCurrentlyExecuted = isBeingExecuted }).Entity;

			await this._context.SaveChangesAsync();
			return history;
        }


		private async Task ModifyHistoryInDb(History historyToModify)
        {
			var entity = _context.History.Attach(historyToModify);
			entity.State = EntityState.Modified;

            await this._context.SaveChangesAsync();
		}

		private string FormatMatrixSize(int rows_1, int columns_1, int rows_2, int columns_2)
        {
			return $"({rows_1} ; {columns_1}) x ({rows_2} ; {columns_2})";
        }

    }
}
