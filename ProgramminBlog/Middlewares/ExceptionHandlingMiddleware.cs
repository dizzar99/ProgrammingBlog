using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProgBlog.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgramminBlog.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await this.next(httpContext);
            }
            catch(ServiceException e)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = e.ErrorCode;
                var errorObj = new { ErrorField = e.Field, ErrorMessage = e.Message };
                var json = JsonConvert.SerializeObject(errorObj);
                httpContext.Response.ContentType = "Application/json";
                await httpContext.Response.WriteAsync(json);
                //await httpContext.Response.WriteAsync(new { ErrorMessage = e.Message});
            }
            catch(Exception e)
            {
                httpContext.Response.StatusCode = 500;
                var json = JsonConvert.SerializeObject(e.Message);
                httpContext.Response.ContentType = "Application/json";
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
