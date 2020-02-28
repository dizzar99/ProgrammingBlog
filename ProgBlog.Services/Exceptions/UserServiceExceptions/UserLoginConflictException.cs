using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserServiceExceptions
{
    public class UserLoginConflictException : ServiceException
    {
        private const string ErrorMessage = "User with same login is already in the service.";
        public UserLoginConflictException() : base(409, ErrorMessage)
        {

        }
    }
}
