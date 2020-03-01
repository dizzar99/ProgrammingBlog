using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProgBlog.DataAccess.Models
{
    public class DbRole
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
