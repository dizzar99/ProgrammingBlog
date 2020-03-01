using ProgBlog.Services.Models.IdentityManagment;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IUserManager
    {
        Task<UserCredentials> GetByLoginAsync(string login);
        Task<AuthenticationResult> RegisterAsync(RegisterUserRequest registerRequest);
        Task<AuthenticationResult> LoginAsync(LoginUserRequest loginRequest);
        Task ChangePasswordAsync(string login, string newPassword);
    }
}
