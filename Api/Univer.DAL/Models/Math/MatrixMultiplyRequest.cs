using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Math
{
    public class MatrixMultiplyRequest
    {
        public int row_1 { get; set; }
        public int column_1 { get; set; }
        public int row_2 { get; set; }
        public int column_2 { get; set; }
    }
}
