using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using ProgBlog.Services.Models.UserManagment;
using ProgramminBlog.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramminBlog.Controllers
{
    [Authorize]
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

        /// <summary>
        /// Get all articles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ArticleListItem>> GetArticlesAsync()
        {
            return await this.articleService.GetArticlesAsync();
        }

        /// <summary>
        /// Get article with specified identifier
        /// </summary>
        /// <param name="id">Article identifier</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ArticleDetails> GetArticleAsync(string id)
        {
            return await this.articleService.GetArticleAsync(id);
        }

        /// <summary>
        /// Creates new article.
        /// </summary>
        /// <param name="createArticle">The <see cref="CreateArticleRequest"/>.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [IdentityFilter]
        public async Task<ArticleDetails> CreateArticleAsync([FromBody] CreateArticleRequest createArticle)
        {
            var user = await this.GetUserAsync(createArticle.UserId);
            var article = await this.articleService.CreateArticleAsync(createArticle);
            await this.userService.AddArticlesToUserAsync(user, new string[] { article.Id });

            return article;
        }

        /// <summary>
        /// Updates existing article.
        /// </summary>
        /// <param name="articleId">Article identifier</param>
        /// <param name="updateArticle">The <see cref="UpdateArticleRequest"/>.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        [IdentityFilter]
        public async Task<ArticleDetails> UpdateArticleAsync(string articleId, [FromBody] UpdateArticleRequest updateArticle)
        {
            var article = await this.articleService.UpdateArticleAsync(articleId, updateArticle);
            return article;
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="articleId">Artile identifier.</param>
        /// <returns></returns>
        [HttpDelete("{articleId}")]
        public async Task DeleteArticleAsync(string articleId)
        {
            var article = await this.articleService.GetArticleAsync(articleId);
            await this.articleService.DeleteArticleAsync(articleId);

            await this.DeleteArticleFromUserAsync(article);
        }

        /// <summary>
        /// Add a new comment to the article
        /// </summary>
        /// <param name="articleId">Article identifier.</param>
        /// <param name="createComment">The <see cref="CreateCommentRequest"/>.</param>
        /// <returns></returns>
        [HttpPost("{articleId}/comments")]
        public async Task<Comment> AddCommentAsync(string articleId, CreateCommentRequest createComment)
        {
            var comment = await this.articleService.AddCommentAsync(articleId, createComment);
            return comment;
        }

        /// <summary>
        /// Updates a comment of an article.
        /// </summary>
        /// <param name="articleId">Article identifier.</param>
        /// <param name="commentId">Comment identifier</param>
        /// <param name="updateRequest">The <see cref="UpdateCommentRequest"/>.</param>
        /// <returns></returns>
        [HttpPut("{articleId}/comments/{commentId}")]
        public async Task<Comment> UpdateCommentAsync(string articleId, string commentId, UpdateCommentRequest updateRequest)
        {
            return await this.articleService.UpdateCommentAsync(articleId, commentId, updateRequest);
        }

        /// <summary>
        /// Deletes a comment from the article.
        /// </summary>
        /// <param name="articleId">Article identifier.</param>
        /// <param name="commentId">Comment identifier.</param>
        /// <returns></returns>
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
