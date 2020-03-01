namespace ProgBlog.Services.Models.ArticleManagment
{
    public class CreateArticleRequest : UpdateArticleRequest
    {
        public string UserId { get; set; }
    }
}
