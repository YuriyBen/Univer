using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Entities
{
    public class History
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string MatrixSizes { get; set; }
        public int Result { get; set; }

        public UserPublicData User { get; set; }

    }
}
