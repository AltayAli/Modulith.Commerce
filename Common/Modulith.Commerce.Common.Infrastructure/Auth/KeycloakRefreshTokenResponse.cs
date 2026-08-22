using System.Text.Json.Serialization;

namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    internal sealed record KeycloakRefreshTokenResponse(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("refresh_token")] string? RefreshToken,
        [property: JsonPropertyName("id_token")] string? IdToken,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);
}
