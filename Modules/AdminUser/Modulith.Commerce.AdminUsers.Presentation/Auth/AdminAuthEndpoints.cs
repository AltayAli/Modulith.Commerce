using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace Modulith.Commerce.AdminUsers.Presentation.Auth
{
    public static class AdminAuthEndpoints
    {
        public static IEndpointRouteBuilder MapAdminAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("auth").WithTags("Auth");

            group.MapGet("login", Login);
            group.MapPost("logout", Logout);
            group.MapGet("me", GetCurrentUser)
                .RequireAuthorization(new AuthorizeAttribute
                {
                    AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme
                });

            return app;
        }

        private static IResult Login(string? returnUrl = null)
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUrl ?? "/swagger" };
            return Results.Challenge(properties, new[] { OpenIdConnectDefaults.AuthenticationScheme });
        }

        private static IResult Logout()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/swagger" };

            return Results.SignOut(properties, new[]
            {
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            });
        }

        private static IResult GetCurrentUser(HttpContext context)
        {
            var user = context.User;

            var subject = user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? user.FindFirst("sub")?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value ?? user.FindFirst("email")?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst("preferred_username")?.Value;

            return Results.Ok(new CurrentAdminUserResponse(subject, email, name));
        }
    }

    public sealed record CurrentAdminUserResponse(string? Subject, string? Email, string? Name);
}
