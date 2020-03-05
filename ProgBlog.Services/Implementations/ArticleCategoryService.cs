using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Exceptions.CategoryServiceExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleCategoryManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public ArticleCategoryService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CategoryDetails> CreateCategory(CreateCategoryRequest createRequest)
        {
            var dbCategory = this.mapper.Map<DbArticleCategory>(createRequest);
            await this.CheckCategoryConflictsAsync(dbCategory);
            dbCategory.Articles = new List<string>();
            await this.context.Categories.InsertOneAsync(dbCategory);
            var result = this.mapper.Map<CategoryDetails>(dbCategory);
            return result;
        }

        public async Task DeleteCategory(string id)
        {
            var result = await this.context.Categories.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount == 0)
            {
                // TODO
                throw new Exception();
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var dbCategoriseCursor = await this.context.Categories.FindAsync(c => true);
            var dbCategories = dbCategoriseCursor.ToEnumerable();
            var result = this.mapper.Map<IEnumerable<DbArticleCategory>, IEnumerable<Category>>(dbCategories);

            return result;
        }

        public async Task<CategoryDetails> GetCategory(string id)
        {
            var dbCategory = await this.GetDbCategory(id);

            return this.mapper.Map<CategoryDetails>(dbCategory);
        }

        public async Task<CategoryDetails> UpdateCategory(string id, CreateCategoryRequest updateRequest)
        {
            var dbCategory = await this.GetDbCategory(id);
            await this.CheckCategoryConflictsAsync(dbCategory);
            this.mapper.Map(updateRequest, dbCategory);
            await this.context.Categories.FindOneAndReplaceAsync(c => c.Id == id, dbCategory);
            return this.mapper.Map<CategoryDetails>(dbCategory);
        }

        public async Task AddArticleToCategory(CategoryDetails category, string articleId)
        {
            var dbCategory = await this.GetDbCategory(category.Id);
            dbCategory.Articles.Add(articleId);
            dbCategory.Articles = dbCategory.Articles.Distinct().ToList();
            await this.context.Categories.FindOneAndReplaceAsync(c => c.Id == category.Id, dbCategory);
        }

        public async Task DeleteArticleFromCategory(CategoryDetails category, string articleId)
        {
            var dbCategory = await this.GetDbCategory(category.Id);
            dbCategory.Articles.Remove(articleId);
            await this.context.Categories.FindOneAndReplaceAsync(c => c.Id == category.Id, dbCategory);
        }

        private async Task CheckCategoryConflictsAsync(DbArticleCategory dbCategory)
        {
            var cursor = await this.context.Categories.FindAsync(c => c.Name == dbCategory.Name);
            var categoriesWithSameName = cursor.ToList();
            if (categoriesWithSameName.Count != 0)
            {
                throw new CategoryNameConflictException();
            }
        }

        private async Task<DbArticleCategory> GetDbCategory(string id)
        {
            var dbCategoryCursor = await this.context.Categories.FindAsync(c => c.Id == id);
            var dbCategory = dbCategoryCursor.FirstOrDefault();
            if (dbCategory is null)
            {
                throw new CategoryNotFoundException();
            }

            return dbCategory;
        }
    }
}
