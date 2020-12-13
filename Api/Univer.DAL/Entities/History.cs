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
        public long MatrixSum { get; set; }
        public bool IsCurrentlyExecuted { get; set; } = false;
        public bool IsCanceled { get; set; } = false;

        public int UserPublicDataId { get; set; }
        public UserPublicData UserPublicData { get; set; }

    }
}
