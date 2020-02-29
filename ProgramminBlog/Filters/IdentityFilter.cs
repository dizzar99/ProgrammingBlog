using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace ProgramminBlog.Filters
{
    public class IdentityFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            const string key = "userId";
            var parameters = context.ActionArguments;
            if (!parameters.ContainsKey(key))
            {
                parameters.Add(key, key);
            }

            var userId = this.GetUserId(context.HttpContext);
            parameters[key] = userId;
        }

        private string GetUserId(HttpContext context)
        {
            var header = context.Request.Headers["Authorization"];
            var jwt = header.First().Split("Bearer ")[1];
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            var userId = token.Claims.ToList().Where(c => c.Type == ClaimTypes.Name).First().Value;

            return userId;
        }
    }
}
