using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserServiceExceptions
{
    public class UserEmailConflictException : ServiceException
    {
        private const string ErrorMessage = "User with same email address is already in the service.";
        public UserEmailConflictException() : base(409, ErrorMessage)
        {

        }
    }
}
