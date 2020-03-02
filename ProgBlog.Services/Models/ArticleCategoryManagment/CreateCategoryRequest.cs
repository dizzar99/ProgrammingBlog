using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleCategoryManagment
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
