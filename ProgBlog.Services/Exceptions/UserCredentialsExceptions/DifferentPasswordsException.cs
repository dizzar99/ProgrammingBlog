using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserCredentialsExceptions
{
    public class DifferentPasswordsException : ServiceException
    {
        private const string ErrorMessage = "Passwords do not match.";
        public DifferentPasswordsException(string errorMessage = ErrorMessage) : base(401, errorMessage)
        {
            this.Field = "Password";
        }
    }
}
