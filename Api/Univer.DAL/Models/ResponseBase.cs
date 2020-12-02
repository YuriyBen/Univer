using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public class ResponseBase<T>
    {
        public ResponeStatusCodes Status { get; set; } = ResponeStatusCodes.Ok;
        public T Data { get; set; }
    }

    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public UserDTO User { get; set; }
    }

}
