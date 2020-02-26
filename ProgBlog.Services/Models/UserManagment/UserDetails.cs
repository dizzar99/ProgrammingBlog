using ProgBlog.Services.Models.ArticleManagment;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.UserManagment
{
    public class UserDetails : UserListItem
    {
        public IList<ArticleListItem> Articles { get; set; }
        public string Role { get; set; }
    }
}
