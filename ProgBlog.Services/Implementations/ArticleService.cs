using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Exceptions.ArticleServiceExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly IArticleCategoryService categoryService;

        public ArticleService(ApplicationContext context, IMapper mapper, IArticleCategoryService categoryService)
        {
            this.context = context;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }


        public async Task<ArticleDetails> CreateArticleAsync(CreateArticleRequest created)
        {
            var category = await this.categoryService.GetCategory(created.CategoryId);
            var dbArticle = this.mapper.Map<DbArticle>(created);
            this.CheckCreateArticleConflicts(dbArticle);

            dbArticle.Comments = new List<DbComment>();
            await this.context.Articles.InsertOneAsync(dbArticle);
            await this.categoryService.AddArticleToCategory(category, dbArticle.Id);
            return this.mapper.Map<ArticleDetails>(dbArticle);
        }

        public async Task<Comment> AddCommentAsync(string articleId, CreateCommentRequest comment)
        {
            var dbComment = this.mapper.Map<DbComment>(comment);
            dbComment.Children = new List<string>();
            if (!string.IsNullOrEmpty(dbComment.ParentCommentId))
            {
                try
                {
                    var parentComment = await this.GetComment(articleId, dbComment.ParentCommentId);
                }
                catch (CommentNotFoundException e)
                {
                    throw new CommentNotFoundException("Incorrect ParentCommentId. " + e.Message);
                }
            }

            var article = await this.GetDbArticle(articleId);

            article.Comments.Add(dbComment);
            await this.context.Articles.FindOneAndReplaceAsync(a => a.Id == articleId, article);

            return this.mapper.Map<Comment>(dbComment);
        }

        public async Task<DbComment> GetComment(string articleId, string commentId)
        {
            var article = await this.GetDbArticle(articleId);


            var comment = article.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment is null)
            {
                throw new CommentNotFoundException();
            }

            return comment;
        }

        public async Task<ArticleDetails> GetArticleAsync(string articleId)
        {
            var article = await this.GetDbArticle(articleId);
            var articleDetails = this.mapper.Map<ArticleDetails>(article);

            return articleDetails;
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync()
        {
            var articles = this.context.Articles.Find(a => true).ToEnumerable();
            var result = this.mapper.Map<IEnumerable<DbArticle>, IEnumerable<Article>>(articles);
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Article>> GetArticlesAsync(IList<string> ids)
        {
            var articles = await this.context.Articles.FindAsync(d => ids.Contains(d.Id));
            return this.mapper.Map<DbArticle[], IEnumerable<Article>>(articles.ToEnumerable().ToArray());
        }

        public async Task DeleteArticleAsync(string articleId)
        {
            var article = await this.context.Articles.FindOneAndDeleteAsync(a => a.Id == articleId);
            if (article is null)
            {
                throw new ArticleNotFoundException();
            }

            var category = await this.categoryService.GetCategory(article.CategoryId);
            await this.categoryService.DeleteArticleFromCategory(category, articleId);
        }

        public async Task DeleteCommentAsync(string articleId, string commentId)
        {
            var article = await this.GetDbArticle(articleId);

            var toDeleteComment = article.Comments.FirstOrDefault(c => c.Id == commentId);
            if (toDeleteComment is null)
            {
                throw new CommentNotFoundException();
            }

            article.Comments.Remove(toDeleteComment);
            await this.context.Articles.FindOneAndReplaceAsync(a => a.Id == articleId, article);
        }

        public async Task<ArticleDetails> UpdateArticleAsync(string articleId, UpdateArticleRequest updateArticle)
        {
            var article = await this.GetDbArticle(articleId);
            if(article.CategoryId != updateArticle.CategoryId)
            {
                var oldCategory = await this.categoryService.GetCategory(article.CategoryId);
                await this.categoryService.DeleteArticleFromCategory(oldCategory, articleId);
            }

            this.mapper.Map(updateArticle, article);
            await this.CheckUpdateArticleConflicts(article);
            var newCategory = await this.categoryService.GetCategory(updateArticle.CategoryId);
            await this.context.Articles.FindOneAndReplaceAsync(a => a.Id == articleId, article);
            await this.categoryService.AddArticleToCategory(newCategory, articleId);
            return this.mapper.Map<ArticleDetails>(article);
        }

        public async Task<Comment> UpdateCommentAsync(string articleId, string commentId, UpdateCommentRequest updateComment)
        {
            var article = await this.GetDbArticle(articleId);

            var comment = article.Comments.FirstOrDefault(c => c.Id == commentId);
            if (comment is null)
            {
                throw new CommentNotFoundException();
            }

            this.mapper.Map(updateComment, comment);
            await this.context.Articles.FindOneAndReplaceAsync(a => a.Id == articleId, article);

            return this.mapper.Map<Comment>(comment);
        }

        private async Task<DbArticle> GetDbArticle(string articleId)
        {
            var articleCursor = await this.context.Articles.FindAsync(a => a.Id == articleId);
            var article = articleCursor.FirstOrDefault();

            return article ?? throw new ArticleNotFoundException();
        }

        private void CheckCreateArticleConflicts(DbArticle article)
        {
            if (this.context.Articles.CountDocuments(a => a.Title == article.Title) > 0)
            {
                throw new ArticleTitleConflictException();
            }
        }

        private async Task CheckUpdateArticleConflicts(DbArticle article)
        {
            var articlesCursor = await this.context.Articles.FindAsync(a => a.Title == article.Title && a.Id != article.Id);
            var articlesWithSameTitle = articlesCursor.ToList();
            if (articlesWithSameTitle.Count > 0)
            {
                throw new ArticleTitleConflictException();
            }
        }
    }
}
