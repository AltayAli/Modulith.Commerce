using System.Text.Json.Serialization;

namespace Modulith.Commerce.Common.Auth;

internal sealed record KeycloakTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("expires_in")] int ExpiresIn);
