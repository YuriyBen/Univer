using System;
using System.Collections.Generic;
using System.Text;
using Univer.DAL.Helpers;

namespace Univer.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } = RoleType.Unverified;

        public virtual UserPublicData UserPublicData { get; set; }

    }
}
