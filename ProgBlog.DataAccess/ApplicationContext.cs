using MongoDB.Driver;
using ProgBlog.DataAccess.Interfaces;
using ProgBlog.DataAccess.Models;

namespace ProgBlog.DataAccess
{
    public class ApplicationContext
    {
        public IMongoCollection<DbArticle> Articles { get; set; }
        public IMongoCollection<DbUser> Users { get; set; }
        public IMongoCollection<DbRole> Roles { get; set; }
        public IMongoCollection<DbArticleCategory> Categories { get; set; }

        public ApplicationContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this.Articles = database.GetCollection<DbArticle>("Articles");
            this.Users = database.GetCollection<DbUser>("Users");
            this.Roles = database.GetCollection<DbRole>("Roles");
            this.Categories = database.GetCollection<DbArticleCategory>("Categories");
        }
    }
}
