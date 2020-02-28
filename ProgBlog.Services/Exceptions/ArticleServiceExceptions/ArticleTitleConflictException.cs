using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.ArticleServiceExceptions
{
    public class ArticleTitleConflictException : ServiceException
    {
        private const string ErrorMessage = "Article with the same title is already in the service.";
        public ArticleTitleConflictException() : base(409, ErrorMessage)
        {

        }
    }
}
