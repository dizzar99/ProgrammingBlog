using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using ProgBlog.DataAccess;
using ProgBlog.DataAccess.Models;
using ProgBlog.Options;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
using ProgBlog.Services.Exceptions.UserServiceExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationContext context;
        private readonly IMapper mapper;
        private readonly JwtSettings jwtSettings;

        public UserManager(ApplicationContext context, IMapper mapper, JwtSettings jwtSettings)
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtSettings = jwtSettings;
        }

        //public async Task<UserCredentials> GetByLoginAsync(string login)
        //{
        //    return await this.GetUserCredentials(login);
        //}

        //public async Task<UserCredentials> CreateUserAsync(RegisterUserRequest registerRequest)
        //{
        //    var dbUser = this.mapper.Map<DbUser>(registerRequest);
        //    this.CheckCreateUserConflicts(dbUser);
        //    const string role = "User";
        //    dbUser.Role = role;
        //    dbUser.Articles = new List<string>();
        //    await this.context.Users.InsertOneAsync(dbUser);

        //    return this.mapper.Map<UserCredentials>(dbUser);
        //}

        public async Task<AuthenticationResult> RegisterAsync(RegisterUserRequest registerRequest)
        {
            var dbUser = this.mapper.Map<DbUser>(registerRequest);
            this.CheckCreateUserConflicts(dbUser);
            const string role = "User";
            dbUser.Role = role;
            dbUser.Articles = new List<string>();
            await this.context.Users.InsertOneAsync(dbUser);

            var credentials = this.mapper.Map<UserCredentials>(dbUser);
            return this.AuthenticateUser(credentials);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginUserRequest loginRequest)
        {
            var credentials = await this.GetUserCredentials(loginRequest.Login);
            if (credentials.Password != loginRequest.Password)
            {
                throw new InvalidUserPasswordException();
            }

            return this.AuthenticateUser(credentials);
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest changePassword, string newPassword)
        {
            var dbUserCursor = await this.context.Users.FindAsync(u => u.Login == changePassword.Login);
            var dbUser = dbUserCursor.First();
            if (dbUser.Email != changePassword.Email)
            {
                throw new DifferentEmailsException();
            }

            dbUser.Password = newPassword;
            await this.context.Users.FindOneAndReplaceAsync(u => u.Id == dbUser.Id, dbUser);
        }

        private async Task<UserCredentials> GetUserCredentials(string login)
        {
            var dbUserCursor = await this.context.Users.FindAsync(u => u.Login == login);
            var dbUser = dbUserCursor.First();
            return this.mapper.Map<UserCredentials>(dbUser);
        }

        private AuthenticationResult AuthenticateUser(UserCredentials credentials)
        {
            var identity = this.GetIdentity(credentials);
            var token = this.GetToken(identity);
            return new AuthenticationResult
            {
                Id = credentials.Id,
                Token = token
            };
        }

        private ClaimsIdentity GetIdentity(UserCredentials credentials)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, credentials.Id),
                new Claim(ClaimTypes.Role, credentials.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimTypes.Name,
                ClaimTypes.Role);

            return claimsIdentity;
        }

        private string GetToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(2)),
                    signingCredentials: new SigningCredentials(this.jwtSettings.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            const string bearer = "Bearer ";

            return bearer + encodedJwt;
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
