using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
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

        public async Task ChangePasswordAsync(ChangePasswordRequest changePassword)
        {
            string newPassword = this.GenerateNewPassword();
            await this.userManager.ChangePasswordAsync(changePassword, newPassword);

            var message = this.GenerateMessage(changePassword.Email, newPassword);
            this.emailSender.SendMail(message);
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

        //private AuthenticationResult AuthenticateUser(UserCredentials credentials)
        //{
        //    var identity = this.GetIdentity(credentials);
        //    var token = this.GetToken(identity);
        //    return new AuthenticationResult
        //    {
        //        Id = credentials.Id,
        //        Token = token
        //    };
        //}

        //private ClaimsIdentity GetIdentity(UserCredentials credentials)
        //{
        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, credentials.Id),
        //        new Claim(ClaimTypes.Role, credentials.Role)
        //    };

        //    ClaimsIdentity claimsIdentity = new ClaimsIdentity(
        //        claims,
        //        "Token",
        //        ClaimTypes.Name,
        //        ClaimTypes.Role);

        //    return claimsIdentity;
        //}

        //private string GetToken(ClaimsIdentity identity)
        //{
        //    var now = DateTime.UtcNow;
        //    var jwt = new JwtSecurityToken(
        //            notBefore: now,
        //            claims: identity.Claims,
        //            expires: now.Add(TimeSpan.FromMinutes(2)),
        //            signingCredentials: new SigningCredentials(this.jwtSetting.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        //    const string bearer = "Bearer ";

        //    return bearer + encodedJwt;
        //}

        private string GenerateNewPassword()
        {
            return "!QAZxsw2";
        }
    }
}
