using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public enum ResponeStatusCodes
    {
        Ok = 0,
        BadRequest = 1,
        InvalidLoginOrPassword = 2,
        UserAlreayExists = 3,
    }
}

