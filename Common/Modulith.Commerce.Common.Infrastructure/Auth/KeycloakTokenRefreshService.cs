using System.Net.Http.Json;

namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    public sealed class KeycloakTokenRefreshService(HttpClient httpClient, KeycloakTokenRefreshOptions options)
        : IKeycloakTokenRefreshService
    {
        public async Task<KeycloakRefreshResult?> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var requestBody = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken,
                ["client_id"] = options.ClientId,
                ["client_secret"] = options.ClientSecret
            };

            using var response = await httpClient.PostAsync(
                options.TokenEndpoint,
                new FormUrlEncodedContent(requestBody),
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {

                return null;
            }

            var payload = await response.Content.ReadFromJsonAsync<KeycloakRefreshTokenResponse>(cancellationToken);

            return payload is null
                ? null
                : new KeycloakRefreshResult(payload.AccessToken, payload.RefreshToken, payload.IdToken, payload.ExpiresIn);
        }
    }
}
