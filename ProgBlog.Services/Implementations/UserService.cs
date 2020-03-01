using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Exceptions.UserServiceExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.ArticleManagment;
using ProgBlog.Services.Models.UserManagment;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<UserDetails> GetUserAsync(string userId)
        {
            var dbUser = await this.GetDbUserAsync(userId);
            var userDetails = this.mapper.Map<UserDetails>(dbUser);

            userDetails.Articles = await this.GetUsersArticles(dbUser.Articles);
            return userDetails;
        }

        public async Task<IEnumerable<UserListItem>> GetUsersAsync()
        {
            var dbUsers = await this.context.Users.FindAsync(user => true);
            return this.mapper.Map<DbUser[], IEnumerable<UserListItem>> (dbUsers.ToEnumerable().ToArray());
        }

        public async Task DeleteUserAsync(string userId)
        {
            var result = await this.context.Users.DeleteOneAsync(user => user.Id == userId);
            if (result.DeletedCount == 0)
            {
                throw new UserNotFoundException();
            }
        }

        public async Task<UserDetails> UpdateUserAsync(string userId, UpdateUserRequest user)
        {
            var dbUser = await this.GetDbUserAsync(userId);
            this.mapper.Map(user, dbUser);

            this.CheckUpdateUserConflicts(dbUser);
            await this.context.Users.FindOneAndReplaceAsync(u => u.Id == userId, dbUser);
            var userDetails = this.mapper.Map<UserDetails>(dbUser);
            userDetails.Articles = await this.GetUsersArticles(dbUser.Articles);
            return userDetails;
        }

        public async Task AddArticlesToUserAsync(UserDetails user, IEnumerable<string> articlesIds)
        {
            var dbUser = await this.GetDbUserAsync(user.Id);
            dbUser.Articles = dbUser.Articles
                .Concat(articlesIds)
                .Distinct()
                .ToList();
            await this.context.Users.FindOneAndReplaceAsync(u => u.Id == user.Id, dbUser);
        }

        public async Task DeleteArticlesFromUserAsync(UserDetails user, IEnumerable<string> articlesIds)
        {
            var dbUser = await this.GetDbUserAsync(user.Id);
            dbUser.Articles = dbUser.Articles.Except(articlesIds).ToList();

            await this.context.Users.FindOneAndReplaceAsync(u => u.Id == user.Id, dbUser);
        }

        private async Task<DbUser> GetDbUserAsync(string userId)
        {
            var usersCursor = await this.context.Users.FindAsync(u => u.Id == userId);
            var user = usersCursor.FirstOrDefault();
            if (user is null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        private async Task<IList<Article>> GetUsersArticles(IList<string> ids)
        {
            var articles = await this.articleService.GetArticlesAsync(ids);
            return articles.ToList();
        }

        private void CheckCreateUserConflicts(DbUser user)
        {
            if(this.context.Users.Find(u => u.Login == user.Login).CountDocuments() > 0)
            {
                throw new UserLoginConflictException();
            }

            if (this.context.Users.Find(u => u.Email == user.Email).CountDocuments() > 0)
            {
                throw new UserEmailConflictException();
            }
        }

        private void CheckUpdateUserConflicts(DbUser checkUser)
        {
            var usersWithSameLogin = this.context.Users.Find(u => u.Login == checkUser.Login && u.Id != checkUser.Id);

            if (usersWithSameLogin.CountDocuments() > 0)
            {
                throw new UserLoginConflictException();
            }

            var usersWithSameEmail = this.context.Users.Find(u => u.Email == checkUser.Email && u.Id != checkUser.Id);
            if (usersWithSameEmail.CountDocuments() > 0)
            {
                throw new UserEmailConflictException();
            }
        }
    }
}
