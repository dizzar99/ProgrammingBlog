using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserCredentialsExceptions
{
    public class InvalidUserPasswordException : ServiceException
    {
        private const string ErrorMessage = "Invalid password.";
        public InvalidUserPasswordException() : base(400, ErrorMessage)
        {
            this.Field = "Password";
        }
    }
}
