using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Exceptions.CategoryServiceExceptions
{
    public class CategoryNotFoundException : ServiceException
    {
        private const string ErrorMessage = "Category not found.";
        public CategoryNotFoundException(string errorMessage = ErrorMessage) : base(404, errorMessage)
        {
            this.Field = "CategoryId";
        }
    }
}
