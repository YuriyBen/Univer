using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univer.DAL;
using Univer.DAL.Entities;
using Univer.DAL.Models.Math;

namespace Univer.BLL.Services
{
    public class MathService : IMathService
    {
        private readonly AppDbContext _context;

        public MathService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<long> MatrixMultiply(MatrixMultiplyRequest matrixMultiplyRequest)
        {
			int userPublicDataId = _context.UsersPublicData.FirstOrDefault(u => u.UserId == matrixMultiplyRequest.UserId).Id;
			string formattedMatrixSizes = this.FormatMatrixSize(matrixMultiplyRequest.rows_1, matrixMultiplyRequest.columns_1, matrixMultiplyRequest.rows_2, matrixMultiplyRequest.columns_2);
			History history = await this.AddMathResultToDb(result: 0, userPublicDataId: userPublicDataId, formattedMatrixSize: formattedMatrixSizes, isBeingExecuted: true);

            Random rand = new Random();
			
			var matrix1 = new int[matrixMultiplyRequest.rows_1, matrixMultiplyRequest.columns_1];
			var matrix2 = new int[matrixMultiplyRequest.rows_2, matrixMultiplyRequest.columns_2];

			for (var i = 0; i < matrixMultiplyRequest.rows_1; i++)
			{
				for (var j = 0; j < matrixMultiplyRequest.columns_1; j++)
				{
					matrix1[i, j] = rand.Next(0, 10); ;
				}
			}
			for (var i = 0; i < matrixMultiplyRequest.rows_2; i++)
			{
				for (var j = 0; j < matrixMultiplyRequest.columns_2; j++)
				{
					matrix2[i, j] = rand.Next(0, 10); ;
				}
			}


			var matrix3 = new int[matrixMultiplyRequest.rows_1, matrixMultiplyRequest.columns_2];

			for (var i = 0; i < matrixMultiplyRequest.rows_1 ; i++)
			{
				for (var j = 0; j < matrixMultiplyRequest.columns_2; j++)
				{
					matrix3[i, j] = 0;

					for (var k = 0; k < matrixMultiplyRequest.columns_1; k++)
					{
						matrix3[i, j] += matrix1[i, k] * matrix2[k, j];
					}
				}
			}

			long sum = 0;

			for (var i = 0; i < matrixMultiplyRequest.rows_1; i++)
			{
				for (var j = 0; j < matrixMultiplyRequest.columns_2; j++)
				{
					sum += matrix3[i, j];
				}
			}

			
			await this.ModifyHistoryInDb(historyToModify: history, result: sum);

            return sum;
		}

        private async Task<History> AddMathResultToDb(int result, int userPublicDataId, string formattedMatrixSize, bool isBeingExecuted = false)
        {
			History history =  this._context.History.Add(new History { Date = DateTime.Now, UserPublicDataId = userPublicDataId, Result = result, MatrixSizes = formattedMatrixSize, IsCurrentlyExecuted = isBeingExecuted }).Entity;

			await this._context.SaveChangesAsync();
			return history;
        }

		private async Task ModifyHistoryInDb(long result, History historyToModify)
        {
            historyToModify.IsCurrentlyExecuted = false;
            historyToModify.Result = result;

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
