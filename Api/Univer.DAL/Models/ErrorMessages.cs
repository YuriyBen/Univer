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

        public const string MatrixSizesEror = "Multiplication is not possible! The number of columns in the first matrix is ​​not equal to the number of rows in the second matrix.";
        public const string LimitOfExecutableTasks = "Amount of executable tasks is overflow. Please, try again later.";

    }
}
