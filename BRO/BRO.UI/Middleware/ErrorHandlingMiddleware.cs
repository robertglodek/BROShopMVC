using BRO.Domain.Utilities.CustomExceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BRO.UI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
            }
            catch(ValidationException ex)
            {
                context.Response.StatusCode = 404;
            }
        }
    }
}
