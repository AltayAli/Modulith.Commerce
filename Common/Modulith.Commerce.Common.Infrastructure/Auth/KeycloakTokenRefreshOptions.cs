namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    public sealed class KeycloakTokenRefreshOptions
    {
        public required string TokenEndpoint { get; init; }
        public required string ClientId { get; init; }
        public required string ClientSecret { get; init; }
    }
}
