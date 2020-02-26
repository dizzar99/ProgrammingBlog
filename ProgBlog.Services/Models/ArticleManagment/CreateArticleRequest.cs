using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class CreateArticleRequest : UpdateArticleRequest
    {
        public string AuthorId { get; set; }
    }
}
