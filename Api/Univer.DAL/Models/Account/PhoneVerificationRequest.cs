using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Account
{
    public class PhoneVerificationRequest
    {
        public int UserId { get; set; }
        public string Phone { get; set; }
        public long SecretKey { get; set; }
    }
}
