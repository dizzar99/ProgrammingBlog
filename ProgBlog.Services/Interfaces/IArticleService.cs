using ProgBlog.Services.Models;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDetails> GetArticleAsync(string articleId);
        Task<IEnumerable<ArticleListItem>> GetArticlesAsync();
        Task<IEnumerable<ArticleListItem>> GetArticlesAsync(IList<string> ids);
        Task<ArticleDetails> CreateArticleAsync(CreateArticleRequest article);
        Task<ArticleDetails> UpdateArticleAsync(string articleId, UpdateArticleRequest article);
        Task DeleteArticleAsync(string articleId);
        Task<Comment> AddCommentAsync(string articleId, CreateCommentRequest commentId);
        Task<Comment> UpdateCommentAsync(string articleId, string commentId, UpdateCommentRequest updateRequest);
        Task DeleteCommentAsync(string articleId, string commentId);
    }
}
