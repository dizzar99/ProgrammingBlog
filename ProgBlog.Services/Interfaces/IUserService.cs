using ProgBlog.Services.Models.UserManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserListItem>> GetUsersAsync();
        Task<UserDetails> GetUserAsync(string userId);
        Task<UserDetails> UpdateUserAsync(string id, UpdateUserRequest user);
        Task DeleteUserAsync(string id);
        Task AddArticlesToUserAsync(UserDetails user, IEnumerable<string> articleId);
        Task RemoveArticlesFromUserAsync(UserDetails user, IEnumerable<string> articleId);
    }
}
