using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
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
        public async Task<IEnumerable<Article>> GetArticlesAsync()
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
        /// <param name="userId">User Identifier.</param>
        /// <param name="createArticle">The <see cref="CreateArticleRequest"/>.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [IdentityFilter]
        public async Task<ArticleDetails> CreateArticleAsync(string userId, [FromBody] CreateArticleRequest createArticle)
        {
            createArticle.UserId = userId;
            var user = await this.GetUserAsync(createArticle.UserId);
            var article = await this.articleService.CreateArticleAsync(createArticle);
            await this.userService.AddArticlesToUserAsync(user, new string[] { article.Id });

            return article;
        }

        /// <summary>
        /// Updates existing article.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="articleId">Article identifier</param>
        /// <param name="updateArticle">The <see cref="UpdateArticleRequest"/>.</param>
        /// <returns></returns>
        [HttpPut("{articleId}")]
        [Authorize]
        [IdentityFilter]
        public async Task<IActionResult> UpdateArticleAsync(string userId, string articleId, [FromBody] UpdateArticleRequest updateArticle)
        {
            var article = await this.articleService.GetArticleAsync(articleId);
            if (article.AuthorId != userId)
            {
                throw new AccessDeniedException("Only author can update his articles");
            }

            var result = await this.articleService.UpdateArticleAsync(articleId, updateArticle);
            return this.Ok(result);
        }

        /// <summary>
        /// Deletes article.
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="articleId">Artile identifier.</param>
        /// <returns></returns>
        [HttpDelete("{articleId}")]
        [Authorize]
        [IdentityFilter]
        public async Task<IActionResult> DeleteArticleAsync(string userId, string articleId)
        {
            var article = await this.articleService.GetArticleAsync(articleId);
            if (article.AuthorId != userId)
            {
                throw new AccessDeniedException("Only author can delete his articles");
            }

            await this.articleService.DeleteArticleAsync(articleId);

            await this.DeleteArticleFromUserAsync(article);
            return this.NoContent();
        }

        /// <summary>
        /// Add a new comment to the article
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="articleId">Article identifier.</param>
        /// <param name="createComment">The <see cref="CreateCommentRequest"/>.</param>
        /// <returns></returns>
        [HttpPost("{articleId}/comments")]
        [Authorize]
        [IdentityFilter]
        public async Task<Comment> AddCommentAsync(string userId, string articleId, CreateCommentRequest createComment)
        {
            createComment.UserId = userId;
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
        [Authorize]
        [IdentityFilter]
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
        [Authorize]
        [IdentityFilter]
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
            await this.userService.DeleteArticlesFromUserAsync(user, new string[] { article.Id });
        }
    }
}
