using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ProgBlog.DataAccess.Models
{
    public class DbArticle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<DbComment> Comments { get; set; }
    }
}
