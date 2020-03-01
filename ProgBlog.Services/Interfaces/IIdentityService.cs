using ProgBlog.Services.Models.IdentityManagment;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(RegisterUserRequest registerRequest);
        Task<AuthenticationResult> LoginAsync(LoginUserRequest loginRequest);
        Task ChangePasswordAsync(ChangePasswordRequest changePassword);
    }
}
