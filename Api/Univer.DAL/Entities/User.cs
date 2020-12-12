using System;
using System.Collections.Generic;
using System.Text;
using Univer.DAL.Helpers;

namespace Univer.DAL.Entities
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Phone { get; set; }
        //public string Phone { get; set; } //TODO confirm yourself by phone?
        public string PasswordHash { get; set; }
        public string Role { get; set; } = RoleType.Unverified;

        //public int UserPublicDataId { get; set; }
        public virtual UserPublicData UserPublicData { get; set; }

    }
    
}
