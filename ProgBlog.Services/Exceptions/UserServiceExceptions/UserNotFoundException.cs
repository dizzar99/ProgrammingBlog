using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.UserServiceExceptions
{
    public class UserNotFoundException : ServiceException
    {
        private const string ErrorMessage = "User with such id not found.";
        public UserNotFoundException(string errorMessage = ErrorMessage, string field = "Id") : base(404, errorMessage)
        {
            this.Field = field;
        }
    }
}
