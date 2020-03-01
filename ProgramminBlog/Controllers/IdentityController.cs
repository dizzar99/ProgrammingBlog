using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.IdentityManagment;
using System.Threading.Tasks;

namespace ProgramminBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest registerRequest)
        {
            var authentication = await this.identityService.RegisterAsync(registerRequest);
            return this.Ok(authentication);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest loginRequest)
        {
            var authentication = await this.identityService.LoginAsync(loginRequest);
            return this.Ok(authentication);
        }

        [HttpPost("changePassword")]
        public async Task ChangePassword(ChangePasswordRequest changePassword)
        {
            await this.identityService.ChangePasswordAsync(changePassword);
        }
    }
}