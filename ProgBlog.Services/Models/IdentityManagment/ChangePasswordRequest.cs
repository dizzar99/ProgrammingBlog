using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.IdentityManagment
{
    public class ChangePasswordRequest
    {
        public string Login { get; set; }
        public string Email { get; set; }
    }
}
