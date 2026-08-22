using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Modulith.Commerce.Common.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null || httpContextAccessor.HttpContext.User is null)
                return null;

            var claim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return null;

            return (Guid)Convert.ChangeType(new Guid(claim.Value), typeof(Guid));
        }
        public static Guid? GetUserId(this HttpContext context)
        {
            if (context is null || context.User is null)
                return null;

            var claim = context.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim is null)
                return null;

            return (Guid)Convert.ChangeType(new Guid(claim.Value), typeof(Guid));
        }
        public static string GetRequestPath(this IHttpContextAccessor httpContextAccessor) => httpContextAccessor?.HttpContext?.Request?.Path ?? "";
    }
}
