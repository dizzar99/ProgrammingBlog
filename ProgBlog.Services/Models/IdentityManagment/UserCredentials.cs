using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.IdentityManagment
{
    public class UserCredentials
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
