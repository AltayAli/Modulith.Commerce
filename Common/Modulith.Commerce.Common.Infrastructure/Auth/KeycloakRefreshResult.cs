namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    public sealed record KeycloakRefreshResult(
    string AccessToken,
    string? RefreshToken,
    string? IdToken,
    int ExpiresIn);
}
