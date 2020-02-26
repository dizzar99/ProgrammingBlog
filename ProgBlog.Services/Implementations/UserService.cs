using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models;
using ProgBlog.Services.Models.UserManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly IArticleService articleService;

        public UserService(ApplicationContext context, IMapper mapper, IArticleService articleService)
        {
            this.context = context;
            this.mapper = mapper;
            this.articleService = articleService;
        }

        public async Task<UserDetails> CreateUser(CreateUserRequest createdUser)
        {
            const string role = "User";
            var dbUser = this.mapper.Map<DbUser>(createdUser);
            dbUser.Role = role;
            dbUser.Articles = new List<string>();
            await this.context.Users.InsertOneAsync(dbUser);

            return this.mapper.Map<UserDetails>(dbUser);
        }

        public async Task<UserDetails> GetUser(string userId)
        {
            var dbUsers = await this.context.Users.FindAsync(u => u.Id == userId);
            var dbUser = dbUsers.FirstOrDefault();
            var userDetails = this.mapper.Map<UserDetails>(dbUser);
            var articles = await this.articleService.GetArticles(dbUser.Articles);
            userDetails.Articles = articles.ToList();
            return userDetails;
        }

        public async Task<IEnumerable<UserListItem>> GetUsers()
        {
            var dbUsers = await this.context.Users.FindAsync(user => true);
            return this.mapper.Map<DbUser[], IEnumerable<UserListItem>> (dbUsers.ToEnumerable().ToArray());
        }

        public Task Remove(string id)
        {
            return this.context.Users.DeleteOneAsync(user => user.Id == id);
        }

        public async Task<UserDetails> UpdateUser(string id, UpdateUserRequest user)
        {
            var d = await this.context.Users.FindAsync(u => u.Id == id);
            var dbUser = d.FirstOrDefault();
            this.mapper.Map(user, dbUser);
            await this.context.Users.FindOneAndReplaceAsync(u => u.Id == id, dbUser);
            return this.mapper.Map<UserDetails>(dbUser);
        }

        public async Task AddArticlesToUser(string userId, IEnumerable<string> articlesIds)
        {
            var updateUser = await this.GetUpdateUserRequest(userId);
            updateUser.Articles = updateUser.Articles
                .Concat(articlesIds)
                .Distinct()
                .ToList();
            await this.UpdateUser(userId, updateUser);
        }

        public async Task RemoveArticlesFromUser(string userId, IEnumerable<string> articlesIds)
        {
            var updateUser = await this.GetUpdateUserRequest(userId);
            updateUser.Articles = updateUser.Articles.Except(articlesIds).ToList();
            await this.UpdateUser(userId, updateUser);
        }

        private async Task<UpdateUserRequest> GetUpdateUserRequest(string userId)
        {
            var user = await this.GetUser(userId);
            var updateUser = this.mapper.Map<UpdateUserRequest>(user);

            return updateUser;
        }
    }
}
