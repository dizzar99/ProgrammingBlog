using Microsoft.AspNetCore.Mvc.Filters;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
using System;
using System.Linq;
using System.Security.Claims;

namespace ProgramminBlog.Filters
{
    public class OwnerOrAdminFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == ClaimTypes.Name).Value;
            var role = claims.First(c => c.Type == ClaimTypes.Role).Value;
            string key = "userId";
            var userIdFromRequest = (string)context.ActionArguments[key];
            if(!(role == "admin" || userId == userIdFromRequest))
            {
                throw new AccessDeniedException();
            }
        }
    }
}
