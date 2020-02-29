using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.IdentityManagment
{
    public class LoginUserRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
