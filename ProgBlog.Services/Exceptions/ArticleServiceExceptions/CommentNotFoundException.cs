using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.ArticleServiceExceptions
{
    public class CommentNotFoundException : ServiceException
    {
        private const string ErrorMessage = "These article doesn't contain the comment with such id.";
        public CommentNotFoundException(string errorMessage = ErrorMessage) : base(404, errorMessage)
        {
            this.Field = "Comment.Id";
        }
    }
}
