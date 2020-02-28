using Microsoft.AspNetCore.Http;
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
                httpContext.Response.StatusCode = e.ErrorCode;
                await httpContext.Response.WriteAsync(e.Message);
            }
            catch(Exception e)
            {
                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsync(e.Message);
            }
        }
    }
}
