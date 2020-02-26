using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class ArticleListItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
