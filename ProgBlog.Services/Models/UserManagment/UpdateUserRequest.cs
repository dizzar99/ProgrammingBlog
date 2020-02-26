using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class UpdateUserRequest : CreateUserRequest
    {
        public IList<string> Articles { get; set; }
    }
}
