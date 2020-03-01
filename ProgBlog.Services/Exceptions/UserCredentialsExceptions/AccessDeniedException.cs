using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserCredentialsExceptions
{
    public class AccessDeniedException : ServiceException
    {
        private const string ErrorMessage = "Access denied";
        public AccessDeniedException(string errorMessage = ErrorMessage) : base(403, errorMessage)
        {
            this.Field = "User";
        }
    }
}
