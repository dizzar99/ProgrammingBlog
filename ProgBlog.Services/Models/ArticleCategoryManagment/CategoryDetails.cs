using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.ArticleCategoryManagment
{
    public class CategoryDetails : Category
    {
        public IList<string> Articles { get; set; }
    }
}
