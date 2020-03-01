using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="registerRequest">The <see cref="RegisterUserRequest"/>.</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterAsync(RegisterUserRequest registerRequest)
        {
            var authentication = await this.identityService.RegisterAsync(registerRequest);
            return this.Ok(authentication);
        }

        /// <summary>
        /// Login the existing user.
        /// </summary>
        /// <param name="loginRequest">The <see cref="LoginUserRequest"/>.</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginAsync(LoginUserRequest loginRequest)
        {
            var authentication = await this.identityService.LoginAsync(loginRequest);
            return this.Ok(authentication);
        }

        [HttpPost("resetpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task ResetPasswordAsync(ResetPasswordRequest changePassword)
        {
            await this.identityService.ResetPasswordAsync(changePassword);
        }

        [HttpPost("changepassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task ChangePasswordAsync(ChangePasswordRequest changePassword)
        {
            await this.identityService.ChangePasswordAsync(changePassword);
        }
    }
}