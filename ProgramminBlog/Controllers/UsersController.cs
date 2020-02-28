using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.UserManagment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramminBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UserListItem>> GetUsers()
        {
            return await this.userService.GetUsersAsync();
        }

        [HttpGet("{id}")]
        public async Task<UserDetails> GetUser(string id)
        {
            return await this.userService.GetUserAsync(id);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<UserDetails> CreateUser(CreateUserRequest user)
        {
            var created = await this.userService.CreateUserAsync(user);
            return created;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<UserListItem> UpdateUser (string id, [FromBody] UpdateUserRequest user)
        {
            var updated = await this.userService.UpdateUserAsync(id, user);
            return updated;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task DeleteUser(string id)
        {
            await this.userService.DeleteUserAsync(id);
        }
    }
}
