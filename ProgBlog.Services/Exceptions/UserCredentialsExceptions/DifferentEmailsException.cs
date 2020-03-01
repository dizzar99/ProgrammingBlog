using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserCredentialsExceptions
{
    public class DifferentEmailsException : ServiceException
    {
        private const string ErrorMessage = "This mail does not match the mail specified during registration";
        public DifferentEmailsException(string errorMessage = ErrorMessage) : base(403, errorMessage)
        {
            this.Field = "Email";
        }
    }
}
