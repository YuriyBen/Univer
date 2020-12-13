using System;
using System.Collections.Generic;
using System.Text;

namespace Univer.DAL.Models
{
    public static class ResponseMessages
    {
        public const string InvalidLoginOrPassword = "Oops..Invalid phone number or password";
        public const string UserAlreayExists = "Oops..User with same phone number already exists";
        public const string TokenIsValid = "Token is valid yet";
        public const string RequestIsCanceled = "Request has been canceled..";

        public const string MatrixSizesEror = "Multiplication is not possible! The number of columns in the first matrix is ​​not equal to the number of rows in the second matrix.";
        public const string LimitOfExecutableTasksForUser = "Amount of executable tasks is overflow. Please, try again later.";
        public const string LimitOfExecutableTasks = "Oops... Server is overflow. Please, wait up to 1 min.";


        public const string UnverifiedUser = "Sorry..You have not verified yourself.";
        public const string BadVerificationSecretKey = "It seems your secret key is not valid.Please, try again.";


        public const string VerifiedUser = "You have verified yourself";
        public const string RepeatedVerification = "New secret key was sent to your phone. Please, try again with new key.";

    }
}
