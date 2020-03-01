namespace ProgBlog.Services.Models.CommentManagment
{
    public class CreateCommentRequest : UpdateCommentRequest
    {
        public string UserId { get; set; }
        public string ParentCommentId { get; set; }
    }
}
