using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Math
{
    public class MatrixMultiplyRequest
    {
        public int UserId { get; set; }
        public int rows_1 { get; set; }
        public int columns_1 { get; set; }
        public int rows_2 { get; set; }
        public int columns_2 { get; set; }
    }
}
