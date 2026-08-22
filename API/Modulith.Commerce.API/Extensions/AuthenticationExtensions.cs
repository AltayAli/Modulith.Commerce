using System.Globalization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Modulith.Commerce.AdminUsers.Infrastructure.Authorization;
using Modulith.Commerce.Common.Infrastructure.Auth;
using Modulith.Commerce.Common.Infrastructure.Authorization;

namespace Modulith.Commerce.API.Extensions;

public static class AuthenticationExtensions
{

    private static readonly TimeSpan SessionHardCap = TimeSpan.FromHours(8);

    private static readonly TimeSpan AccessTokenRefreshSkew = TimeSpan.FromSeconds(30);

    private const string LoginUtcItemKey = "modulith-commerce:login_utc";

    public static IServiceCollection AddModulithCommerceAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var keycloakOptions = configuration
            .GetSection(KeycloakOptions.SectionName)
            .Get<KeycloakOptions>()
            ?? throw new InvalidOperationException("Keycloak configuration section is missing.");

        var keycloakWebClientOptions = configuration
            .GetSection(KeycloakWebClientOptions.SectionName)
            .Get<KeycloakWebClientOptions>()
            ?? throw new InvalidOperationException("Keycloak:Web configuration section is missing.");

        services.AddSingleton(keycloakOptions);
        services.AddSingleton(keycloakWebClientOptions);

        services.AddSingleton<ITicketStore, RedisTicketStore>();

        services.AddSingleton(new KeycloakTokenRefreshOptions
        {
            TokenEndpoint = $"{keycloakOptions.Authority.TrimEnd('/')}/protocol/openid-connect/token",
            ClientId = keycloakWebClientOptions.ClientId,
            ClientSecret = keycloakWebClientOptions.ClientSecret
        });
        services.AddHttpClient<IKeycloakTokenRefreshService, KeycloakTokenRefreshService>();

        services
            .AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
            .Configure<ITicketStore, IKeycloakTokenRefreshService>((options, store, tokenRefreshService) =>
            {
                options.SessionStore = store;
                options.Cookie.Name = "modulith-commerce-session";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.ExpireTimeSpan = SessionHardCap;

                options.SlidingExpiration = false;

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
                options.Events.OnValidatePrincipal = async context =>
                    await ValidatePrincipalWithHardCapAndRefreshAsync(context, tokenRefreshService);
            });

        services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(options =>
            {
                options.Authority = keycloakOptions.Authority;
                options.ClientId = keycloakWebClientOptions.ClientId;
                options.ClientSecret = keycloakWebClientOptions.ClientSecret;
                options.CallbackPath = keycloakWebClientOptions.CallbackPath;
                options.SignedOutCallbackPath = keycloakWebClientOptions.SignedOutCallbackPath;
                options.ResponseType = "code";
                options.UsePkce = true;
                options.SaveTokens = true;

                options.PushedAuthorizationBehavior = PushedAuthorizationBehavior.Disable;
                options.RequireHttpsMetadata = false;
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("email");

                options.Scope.Add("offline_access");

                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                options.Events = new OpenIdConnectEvents
                {

                    OnTicketReceived = context =>
                    {
                        context.Properties!.Items[LoginUtcItemKey] =
                            DateTimeOffset.UtcNow.ToString("o", CultureInfo.InvariantCulture);
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddScoped<IClaimsTransformation, PermissionClaimsTransformation>();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionsAuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization();

        return services;
    }

    private static async Task ValidatePrincipalWithHardCapAndRefreshAsync(
    CookieValidatePrincipalContext context,
    IKeycloakTokenRefreshService tokenRefreshService)
    {
        var properties = context.Properties;
        var utcNow = DateTimeOffset.UtcNow;

        if (!TryResolveLoginUtc(properties, out var loginUtc))
        {

            await RejectAndSignOutAsync(context);
            return;
        }

        var hardCapUtc = loginUtc.Add(SessionHardCap);
        if (utcNow >= hardCapUtc)
        {

            await RejectAndSignOutAsync(context);
            return;
        }

        var accessTokenStillFresh =
            DateTimeOffset.TryParse(
                properties.GetTokenValue("expires_at"),
                CultureInfo.InvariantCulture,
                DateTimeStyles.RoundtripKind,
                out var accessTokenExpiresAt)
            && utcNow < accessTokenExpiresAt.Subtract(AccessTokenRefreshSkew);

        if (accessTokenStillFresh)
        {
            return;
        }

        var refreshToken = properties.GetTokenValue("refresh_token");
        if (string.IsNullOrEmpty(refreshToken))
        {

            await RejectAndSignOutAsync(context);
            return;
        }

        var refreshResult = await tokenRefreshService.RefreshAsync(refreshToken, context.HttpContext.RequestAborted);
        if (refreshResult is null)
        {

            await RejectAndSignOutAsync(context);
            return;
        }

        properties.UpdateTokenValue("access_token", refreshResult.AccessToken);
        properties.UpdateTokenValue("refresh_token", refreshResult.RefreshToken ?? refreshToken);
        if (refreshResult.IdToken is not null)
        {
            properties.UpdateTokenValue("id_token", refreshResult.IdToken);
        }

        var newAccessTokenExpiresAt = utcNow.AddSeconds(refreshResult.ExpiresIn);
        properties.UpdateTokenValue("expires_at", newAccessTokenExpiresAt.ToString("o", CultureInfo.InvariantCulture));

        var candidateExpiresUtc = utcNow.Add(SessionHardCap);
        properties.ExpiresUtc = candidateExpiresUtc < hardCapUtc ? candidateExpiresUtc : hardCapUtc;

        context.ShouldRenew = true;
    }

    private static bool TryResolveLoginUtc(AuthenticationProperties properties, out DateTimeOffset loginUtc)
    {
        if (properties.Items.TryGetValue(LoginUtcItemKey, out var raw) &&
            DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out loginUtc))
        {
            return true;
        }

        if (properties.IssuedUtc is { } issuedUtc)
        {
            loginUtc = issuedUtc;
            return true;
        }

        loginUtc = default;
        return false;
    }

    private static async Task RejectAndSignOutAsync(CookieValidatePrincipalContext context)
    {
        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
