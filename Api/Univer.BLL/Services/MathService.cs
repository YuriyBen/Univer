using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
		private const int AmountOfSimulateniouslyExecutedUserTasks = 2;
		private const int AmountOfSimulateniouslyExecutedTasks = 5;
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
		public async Task<object> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest, CancellationToken cancellationToken)
        {
			int rows_1 = matrixMultiplyRequest.Matrix1.GetUpperBound(0) + 1;
			int columns_1 = matrixMultiplyRequest.Matrix1[0].Length;
			int rows_2 = matrixMultiplyRequest.Matrix2.GetUpperBound(0) + 1;
			int columns_2 = matrixMultiplyRequest.Matrix2[0].Length;

			if (columns_1 != rows_2)
			{
				return new ResponseBase<string> { Status = ResponeStatusCodes.BadRequest, Data = ResponseMessages.MatrixSizesEror };
			}

			try
			{
				int userPublicDataId = _context.UsersPublicData.FirstOrDefault(u => u.UserId == matrixMultiplyRequest.UserId).Id;

				int amountOFAllExecutableTasks = await this._context.History.CountAsync(history => history.IsCurrentlyExecuted);

				if (amountOFAllExecutableTasks == AmountOfSimulateniouslyExecutedTasks)
				{
					return new ResponseBase<string> { Status = ResponeStatusCodes.LimitOfExecutableTasks, Data = ResponseMessages.LimitOfExecutableTasks };
				}

				int amountOfSimultaneouslyExecutedTasks = await this._context.History.Where(item => item.UserPublicDataId == userPublicDataId && item.IsCurrentlyExecuted).CountAsync();

				if (amountOfSimultaneouslyExecutedTasks + 1 /*+1 means +1 thread for current task*/ > AmountOfSimulateniouslyExecutedUserTasks)
				{
					return new ResponseBase<string> { Status = ResponeStatusCodes.LimitOfExecutableTasks, Data = ResponseMessages.LimitOfExecutableTasksForUser };
				}

				string formattedMatrixSizes = this.FormatMatrixSize(rows_1, columns_1, rows_2, columns_2);
				History history = await this.AddMathResultToDb(result: 0, userPublicDataId: userPublicDataId, formattedMatrixSize: formattedMatrixSizes, isBeingExecuted: true);

				var matrix3 = new int[rows_1,columns_2];

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
							matrix3[i, j] += matrixMultiplyRequest.Matrix1[i][k] * matrixMultiplyRequest.Matrix2[k][j];
							if (cancellationToken.IsCancellationRequested)
							{
								await this.PreviousStateDueToCanceledRequest(history: history, cancellationToken: cancellationToken);
							}
						}
					}
				}

				long matrixSum = 0;

				for (var i = 0; i < rows_1; i++)
				{
					for (var j = 0; j < columns_2; j++)
					{
						matrixSum += matrix3[i, j];
						if (cancellationToken.IsCancellationRequested)
						{
							await this.PreviousStateDueToCanceledRequest(history: history, cancellationToken: cancellationToken);
						}

					}
				}
				history.ResultMatrix = JsonConvert.SerializeObject(matrix3);
				history.IsCurrentlyExecuted = false;
				history.MatrixSum = matrixSum;
				await this.ModifyHistoryInDb(historyToModify: history);

				return new ResponseBase<MatrixMultiplyResponse> { Data = new MatrixMultiplyResponse { MatrixSum = matrixSum, ResultMatrix = Newtonsoft.Json.JsonConvert.DeserializeObject<int[][]>(history.ResultMatrix) } };

			}
			catch (Exception ex)
			{
				return new ResponseBase<string> { Status = ResponeStatusCodes.UnexpectedServerError, Data = $"Ooops. {ex.Message}" };

			}

		}

        private async Task<History> AddMathResultToDb(int result, int userPublicDataId, string formattedMatrixSize, bool isBeingExecuted = false)
        {
			History history =  this._context.History.Add(new History { Date = DateTime.Now, UserPublicDataId = userPublicDataId, MatrixSum = result, MatrixSizes = formattedMatrixSize, IsCurrentlyExecuted = isBeingExecuted }).Entity;

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
