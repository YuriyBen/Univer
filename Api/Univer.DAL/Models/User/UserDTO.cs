using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public class UserDTO
    {
        public int Id { get; set; } = 0;
        public string Phone { get; set; }
        public string UserName { get; set; }

    }

    public class RegisterResponse: UserDTO
    {
        public long SecretKey { get; set; }
    }
}
