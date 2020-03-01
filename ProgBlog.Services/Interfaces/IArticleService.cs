using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.CommentManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IArticleService
    {
        Task<ArticleDetails> GetArticleAsync(string articleId);
        Task<IEnumerable<Article>> GetArticlesAsync();
        Task<IEnumerable<Article>> GetArticlesAsync(IList<string> ids);
        Task<ArticleDetails> CreateArticleAsync(CreateArticleRequest article);
        Task<ArticleDetails> UpdateArticleAsync(string articleId, UpdateArticleRequest article);
        Task DeleteArticleAsync(string articleId);
        Task<Comment> AddCommentAsync(string articleId, CreateCommentRequest commentId);
        Task<Comment> UpdateCommentAsync(string articleId, string commentId, UpdateCommentRequest updateRequest);
        Task DeleteCommentAsync(string articleId, string commentId);
    }
}
