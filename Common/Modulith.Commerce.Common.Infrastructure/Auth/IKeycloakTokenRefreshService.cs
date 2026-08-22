namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    public interface IKeycloakTokenRefreshService
    {
        Task<KeycloakRefreshResult?> RefreshAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
