using ProgBlog.Services.Models.IdentityManagment;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IUserManager
    {
        Task<UserCredentials> GetByLoginAsync(string login);
        Task<UserCredentials> CreateUserAsync(RegisterUserRequest registerRequest);
        Task ChangePasswordAsync(string login, string newPassword);
    }
}
