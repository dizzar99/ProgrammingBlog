using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.Services.Models.CommentManagment
{
    public class Comment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public string ParentCommentId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
