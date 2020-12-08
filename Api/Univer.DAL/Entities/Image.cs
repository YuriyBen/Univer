using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Entities
{
    public class Image
    {
        public long Id { get; set; }

        public byte[] Data { get; set; }


        public int UserPublicDataId { get; set; }
        public UserPublicData UserPublicData { get; set; }

    }
}
