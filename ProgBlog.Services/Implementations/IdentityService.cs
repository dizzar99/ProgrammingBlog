using Microsoft.IdentityModel.Tokens;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProgBlog.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IUserManager userManager;
        private readonly IEmailSender emailSender;

        public IdentityService(IUserManager userManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public async Task<AuthenticationResult> RegisterAsync(RegisterUserRequest registerRequest)
        {
            return await this.userManager.RegisterAsync(registerRequest);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginUserRequest loginRequest)
        {
            return await this.userManager.LoginAsync(loginRequest);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest resetPassword)
        {
            string newPassword = this.GenerateNewPassword();
            var credentials = await this.userManager.GetByLoginAsync(resetPassword.Login);
            if (credentials.Email != resetPassword.Email)
            {
                throw new DifferentEmailsException();
            }

            await this.userManager.ChangePasswordAsync(resetPassword.Login, newPassword);

            var message = this.GenerateMessage(resetPassword.Email, newPassword);
            this.emailSender.SendMail(message);
        }

        public async Task ChangePasswordAsync(ChangePasswordRequest changePassword)
        {
            var credentials = await this.userManager.GetByLoginAsync(changePassword.Login);
            if (changePassword.OldPassword != credentials.Password)
            {
                throw new DifferentPasswordsException();
            }

            await this.userManager.ChangePasswordAsync(changePassword.Login, changePassword.NewPassword);
        }

        private Message GenerateMessage(string email, string content)
        {
            string subject = "Change your password";
            var message = new Message
            {
                To = email,
                Subject = subject,
                Content = content
            };

            return message;
        }

        private string GenerateNewPassword()
        {
            return "!QAZxsw2";
        }
    }
}
