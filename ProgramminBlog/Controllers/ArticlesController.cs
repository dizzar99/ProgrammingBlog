using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using ProgBlog.Services.Models.UserManagment;

namespace ProgramminBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public ArticlesController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<IEnumerable<ArticleListItem>> GetArticlesAsync()
        {
            return await this.articleService.GetArticlesAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ArticleDetails> GetArticleAsync(string id)
        {
            return await this.articleService.GetArticleAsync(id);
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<ArticleDetails> CreateArticleAsync([FromBody] CreateArticleRequest createArticle)
        {
            var user = await this.GetUserAsync(createArticle.AuthorId);
            var article = await this.articleService.CreateArticleAsync(createArticle);
            await this.userService.AddArticlesToUserAsync(user, new string[] { article.Id });

            return article;
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<ArticleDetails> UpdateArticleAsync(string id, [FromBody] UpdateArticleRequest updateArticle)
        {
            var article = await this.articleService.UpdateArticleAsync(id, updateArticle);
            return article;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{articleId}")]
        public async Task DeleteArticleAsync(string articleId)
        {
            var article = await this.articleService.GetArticleAsync(articleId);
            await this.articleService.DeleteArticleAsync(articleId);

            await this.DeleteArticleFromUserAsync(article);
        }

        [HttpPost("{articleId}/comments")]
        public async Task<Comment> AddCommentAsync(string articleId, CreateCommentRequest createComment)
        {
            var comment = await this.articleService.AddCommentAsync(articleId, createComment);
            return comment;
        }

        [HttpPut("{articleId}/comments/{commentId}")]
        public async Task<Comment> GetCommentAsync(string articleId, string commentId, UpdateCommentRequest updateRequest)
        {
            return await this.articleService.UpdateCommentAsync(articleId, commentId, updateRequest);
        }

        [HttpDelete("{articleId}/comments/{commentId}")]
        public async Task DeleteCommentAsync(string articleId, string commentId)
        {
            await this.articleService.DeleteCommentAsync(articleId, commentId);
        }

        private async Task<UserDetails> GetUserAsync(string userId)
        {
            return await this.userService.GetUserAsync(userId);
        }

        private async Task DeleteArticleFromUserAsync(ArticleDetails article)
        {
            var user = await this.GetUserAsync(article.AuthorId);
            await this.userService.RemoveArticlesFromUserAsync(user, new string[] { article.Id });
        }
    }
}
