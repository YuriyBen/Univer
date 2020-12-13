using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Math
{
    public class MatrixMultiplyRequest
    {
        public int UserId { get; set; }

        public int [][] Matrix1 { get; set; }
        public int [][] Matrix2 { get; set; }

    }


}
