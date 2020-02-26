using ProgBlog.Services.Models.CommentManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetComment(string commentId);
        Task<CommentDetails> GetCommentDetails(string commentId);
        Task<Comment> CreateComment(CreateCommentRequest article);
        Task<Comment> UpdateComment(string articleId, UpdateCommentRequest article);
        Task Remove(string articleId, string parentId);
    }
}
