using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.CategoryServiceExceptions
{
    public class CategoryNameConflictException : ServiceException
    {
        private const string ErrorMessage = "Category with same name is already in service.";
        public CategoryNameConflictException(string errorMessage = ErrorMessage) : base(409, errorMessage)
        {
            this.Field = "Category.Name";
        }
    }
}
