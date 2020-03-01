using Microsoft.IdentityModel.Tokens;
using ProgBlog.Options;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserManager userManager;
        private readonly JwtSettings jwtSetting;
        private readonly IEmailSender emailSender;

        public IdentityService(IUserManager userManager, JwtSettings jwtSetting, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.jwtSetting = jwtSetting;
            this.emailSender = emailSender;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterUserRequest registerRequest)
        {
            var credentials = await this.userManager.CreateUserAsync(registerRequest);

            return this.AuthenticateUser(credentials);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginUserRequest loginRequest)
        {
            var credentials = await this.userManager.GetByLoginAsync(loginRequest.Login);
            if (credentials.Password.CompareTo(loginRequest.Password) != 0)
            {
                throw new InvalidUserPasswordException();
            }

            return this.AuthenticateUser(credentials);
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest changePassword)
        {
            var credentials = await this.userManager.GetByLoginAsync(changePassword.Login);
            if (credentials.Email != changePassword.Email)
            {
                // TODO: exception
            }

            string newPassword = this.GenerateNewPassword();
            await this.userManager.ChangePasswordAsync(credentials.Login, newPassword);
            string subject = "Change your password";
            var message = new Message
            {
                To = changePassword.Email,
                Subject = subject,
                Content = newPassword
            };

            this.emailSender.SendMail(message);
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
                    signingCredentials: new SigningCredentials(this.jwtSetting.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            const string bearer = "Bearer ";

            return bearer + encodedJwt;
        }

        private string GenerateNewPassword()
        {
            return "!QAZxsw2";
        }
    }
}
