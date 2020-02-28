using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserServiceExceptions
{
    public class UserNotFoundException : ServiceException
    {
        private const string ErrorMessage = "User with such id not found.";
        public UserNotFoundException() : base(404, ErrorMessage)
        {

        }
    }
}
