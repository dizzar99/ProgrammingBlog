using ProgBlog.Services.Models.ArticleCategoryManagment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IArticleCategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<CategoryDetails> GetCategory(string id);
        Task<CategoryDetails> CreateCategory(CreateCategoryRequest createRequest);
        Task<CategoryDetails> UpdateCategory(string id, CreateCategoryRequest updateRequest);
        Task DeleteCategory(string id);
        Task AddArticleToCategory(CategoryDetails category, string articleId);
        Task DeleteArticleFromCategory(CategoryDetails category, string articleId);
    }
}
