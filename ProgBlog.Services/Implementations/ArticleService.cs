using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public ArticleService(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ArticleDetails> CreateArticle(CreateArticleRequest created)
        {
            var dbArticle = this.mapper.Map<DbArticle>(created);
            await this.context.Articles.InsertOneAsync(dbArticle);

            return this.mapper.Map<ArticleDetails>(dbArticle);
        }

        public async Task<ArticleDetails> GetArticle(string articleId)
        {
            var articles = await this.context.Articles.FindAsync(a => a.Id == articleId);
            return this.mapper.Map<ArticleDetails>(articles.First());
        }

        public async Task<IEnumerable<ArticleListItem>> GetArticles(int count)
        {
            var articles = this.context.Articles.AsQueryable().Reverse().Take(count).ToArray();
            var result = this.mapper.Map<DbArticle[], IEnumerable<ArticleListItem>>(articles);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<ArticleListItem>> GetArticles(IList<string> ids)
        {
            var articles = await this.context.Articles.FindAsync(d => ids.Contains(d.Id));
            return this.mapper.Map<DbArticle[], IEnumerable<ArticleListItem>>(articles.ToEnumerable().ToArray());
        }

        public async Task Remove(string articleId)
        {
            var article = await this.context.Articles.FindOneAndDeleteAsync(a => a.Id == articleId);
        }

        public async Task<ArticleDetails> UpdateArticle(string articleId, UpdateArticleRequest updateArticle)
        {
            var articleCursor = await this.context.Articles.FindAsync(a => a.Id == articleId);
            var article = articleCursor.First();
            this.mapper.Map(updateArticle, article);
            await this.context.Articles.FindOneAndReplaceAsync(a => a.Id == articleId, article);

            return this.mapper.Map<ArticleDetails>(article);
        }
    }
}
