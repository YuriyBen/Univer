using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public static class ErrorMessages
    {
        public const string InvalidLoginOrPassword = "Oops..Invalid email or password";
        public const string UserAlreayExists = "Oops..User with same email already exists";
        public const string TokenIsValid = "Token is valid yet";
        public const string RequestIsCanceled = "Request has been canceled..";
    }
}
