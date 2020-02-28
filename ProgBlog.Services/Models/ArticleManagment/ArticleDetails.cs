﻿using ProgBlog.Services.Models.CommentManagment;
using System.Collections.Generic;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class ArticleDetails : ArticleListItem
    {
        public string Content { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
