using ProgBlog.Services.Models.UserManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgBlog.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserListItem>> GetUsers();
        Task<UserDetails> GetUser(string userId);
        Task<UserDetails> CreateUser(CreateUserRequest user);
        Task<UserDetails> UpdateUser(string id, UpdateUserRequest user);
        Task Remove(string id);
        Task AddArticlesToUser(string userId, IEnumerable<string> articleId);
        Task RemoveArticlesFromUser(string userId, IEnumerable<string> articleId);
    }
}
