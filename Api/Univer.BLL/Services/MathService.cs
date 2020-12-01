using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.BLL.Services
{
    public class MathService : IMathService
    {
        public int MatrixMultiply(int rows_1, int columns_1, int rows_2, int columns_2)
        {
			Random rand = new Random();
			
			var matrix1 = new int[rows_1, columns_1];
			var matrix2 = new int[rows_2, columns_2];
			int sum = 0;

			for (var i = 0; i < rows_1; i++)
			{
				for (var j = 0; j < columns_1; j++)
				{
					matrix1[i, j] = rand.Next(0, 10); ;
				}
			}
			for (var i = 0; i < rows_2; i++)
			{
				for (var j = 0; j < columns_2; j++)
				{
					matrix2[i, j] = rand.Next(0, 10); ;
				}
			}

			if (columns_1 != rows_2)
			{
				throw new Exception(
					"Multiplication is not possible! " +
					"The number of columns in the first matrix is ​​not equal to the number of rows in the second matrix.");
			}

			var matrix3 = new int[rows_1, columns_2];

			for (var i = 0; i < rows_1 ; i++)
			{
				for (var j = 0; j < columns_2; j++)
				{
					matrix3[i, j] = 0;

					for (var k = 0; k < columns_1; k++)
					{
						matrix3[i, j] += matrix1[i, k] * matrix2[k, j];
					}
				}
			}
			for (var i = 0; i < rows_1; i++)
			{
				for (var j = 0; j < columns_2; j++)
				{
					sum += matrix3[i, j];
				}
			}

			return sum;
		}

    }
}
