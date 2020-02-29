using AutoMapper;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Services.Exceptions.UserServiceExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;

        public UserManager(ApplicationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserCredentials> GetByLoginAsync(string login)
        {
            var dbUserCursor = await this.context.Users.FindAsync(u => u.Login == login);
            var dbUser = dbUserCursor.FirstOrDefault();

            return this.mapper.Map<UserCredentials>(dbUser);
        }

        public async Task<UserCredentials> CreateUserAsync(RegisterUserRequest registerRequest)
        {
            var dbUser = this.mapper.Map<DbUser>(registerRequest);
            this.CheckCreateUserConflicts(dbUser);
            const string role = "User";
            dbUser.Role = role;
            dbUser.Articles = new List<string>();
            await this.context.Users.InsertOneAsync(dbUser);

            return this.mapper.Map<UserCredentials>(dbUser);
        }

        private void CheckCreateUserConflicts(DbUser user)
        {
            if (this.context.Users.Find(u => u.Login == user.Login).CountDocuments() > 0)
            {
                throw new UserLoginConflictException();
            }

            if (this.context.Users.Find(u => u.Email == user.Email).CountDocuments() > 0)
            {
                throw new UserEmailConflictException();
            }
        }
    }
}
