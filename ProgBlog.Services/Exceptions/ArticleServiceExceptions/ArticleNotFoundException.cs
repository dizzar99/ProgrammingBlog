using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.ArticleServiceExceptions
{
    public class ArticleNotFoundException : ServiceException
    {
        private const string ErrorMessage = "Article with such id not found.";
        public ArticleNotFoundException() : base(404, ErrorMessage)
        {

        }
    }
}
