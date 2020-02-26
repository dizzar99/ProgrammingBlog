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
        public async Task<IEnumerable<UserListItem>> Get()
        {
            return await this.userService.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<UserListItem> Get(string id)
        {
            return await this.userService.GetUser(id);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<UserListItem> CreateUser(CreateUserRequest user)
        {
            var created = await this.userService.CreateUser(user);
            return created;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<UserListItem> Put(string id, [FromBody] UpdateUserRequest user)
        {
            var updated = await this.userService.UpdateUser(id, user);
            return updated;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await this.userService.Remove(id);
        }
    }
}
