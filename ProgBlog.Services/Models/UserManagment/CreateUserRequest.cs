using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class CreateUserRequest : UpdateUserRequest
    {
        public string Password { get; set; }

    }
}
