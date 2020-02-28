using MongoDB.Driver;
using ProgBlog.DataAccess.Interfaces;
using ProgBlog.DataAccess.Models;
using ProgBlog.DataAccess.Test;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProgBlog.DataAccess
{
    public class ApplicationContext
    {
        public IMongoCollection<DbArticle> Articles { get; set; }
        public IMongoCollection<DbUser> Users { get; set; }
        public IMongoCollection<DbRole> Roles { get; set; }

        public ApplicationContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            this.Articles = database.GetCollection<DbArticle>("Articles");
            this.Users = database.GetCollection<DbUser>("Users");
            this.Roles = database.GetCollection<DbRole>("Roles");
        }
    }
}
