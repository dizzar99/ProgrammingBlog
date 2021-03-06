﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgBlog.Services.Interfaces;
using ProgBlog.Services.Models.UserManagment;
using ProgramminBlog.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramminBlog.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all registered users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<UserListItem>> GetUsers()
        {
            return await this.userService.GetUsersAsync();
        }

        /// <summary>
        /// Get user with specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UserDetails> GetUser(string id)
        {
            return await this.userService.GetUserAsync(id);
        }

        /// <summary>
        /// Updates user with specified identifier.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/users
        ///     {
        ///        "Login": "NewLogin",
        ///        "Email": someemail@mail.com,
        ///     }
        ///
        /// </remarks>
        /// <param name="userId">Identifier.</param>
        /// <param name="user">User fields to update.</param>
        /// <returns></returns>
        [HttpPut("{userId}")]
        [Authorize]
        [AccountOwnerFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserListItem>> UpdateUser(string userId, [FromBody] UpdateUserRequest user)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState.Values);
            }

            var updated = await this.userService.UpdateUserAsync(userId, user);
            return this.Ok(updated);
        }

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [Authorize]
        [AccountOwnerFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(string userId)
        {
            await this.userService.DeleteUserAsync(userId);
            return this.NoContent();
        }
    }
}
