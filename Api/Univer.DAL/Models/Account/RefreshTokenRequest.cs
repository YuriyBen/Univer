using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models.Account
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }
    }

    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
