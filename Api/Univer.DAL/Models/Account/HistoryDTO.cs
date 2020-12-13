using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Account
{
    public class HistoryDTO
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string MatrixSizes { get; set; }
        public int[][] ResultMatrix { get; set; }
        public long Result { get; set; }
        //public bool IsCurrentlyExecuted { get; set; }
        //public bool IsCanceled { get; set; }
        public string Status { get; set; }
    }
}
