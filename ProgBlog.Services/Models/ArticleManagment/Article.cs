using System;

namespace ProgBlog.Services.Models.ArticleManagment
{
    public class Article
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
