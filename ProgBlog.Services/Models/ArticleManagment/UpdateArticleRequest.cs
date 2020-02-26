using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class UpdateArticleRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
