using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class CreateUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
