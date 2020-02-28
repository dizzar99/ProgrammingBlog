using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.DataAccess.Models
{
    public class DbComment
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }
        public string ParentCommentId { get; set; }
        public IList<string> Children { get; set; }
        public DateTime CreatedDate { get; set; }

        public DbComment()
        {
            this.Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
