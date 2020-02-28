using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class UpdateUserRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
