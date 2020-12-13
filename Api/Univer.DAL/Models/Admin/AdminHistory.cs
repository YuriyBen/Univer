using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Admin
{
    public class AdminHistory
    {
        public string Username { get; set; }
        public string Date { get; set; }
        public string MatrixSizes { get; set; }
        public long Result { get; set; }
        public string Status { get; set; }

    }
}
