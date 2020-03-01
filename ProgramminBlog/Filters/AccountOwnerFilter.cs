using Microsoft.AspNetCore.Mvc.Filters;
using ProgBlog.Services.Exceptions.UserCredentialsExceptions;
using System;
using System.Linq;
using System.Security.Claims;

namespace ProgramminBlog.Filters
{
    public class AccountOwnerFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var claims = context.HttpContext.User.Claims;
            var userId = claims.First(c => c.Type == ClaimTypes.Name).Value;
            string key = "userId";
            var userIdFromRequest = (string)context.ActionArguments[key];
            if(userId != userIdFromRequest)
            {
                throw new AccessDeniedException();
            }
        }
    }
}
