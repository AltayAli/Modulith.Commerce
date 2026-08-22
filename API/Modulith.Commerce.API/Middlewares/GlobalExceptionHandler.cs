using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Modulith.Commerce.API.Middlewares
{
    public class GlobalExceptionHandler(
                ILogger<GlobalExceptionHandler> _logger,
                IProblemDetailsService _problemDetailsService
        ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An unhandled exception has occurred while processing the request.");

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await _problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Type = "https://tools.ietf.org/html/rfc9110#section-15.6.1 ",
                    Detail = "An unexpected error occurred while processing your request. Please try again later."
                }
            });

            return true;
        }
    }
}
