using System.Net.Http.Json;

namespace Modulith.Commerce.Common.Auth;

public sealed class KeycloakTokenProvider(IHttpClientFactory httpClientFactory, KeycloakClientOptions options)
{
    internal const string HttpClientName = "KeycloakAuth";

    private readonly SemaphoreSlim _lock = new(1, 1);
    private string? _cachedToken;
    private DateTimeOffset _expiresAt;

    public async Task<string> GetAdminAccessTokenAsync(CancellationToken cancellationToken)
    {
        if (_cachedToken is not null && DateTimeOffset.UtcNow < _expiresAt)
            return _cachedToken;

        await _lock.WaitAsync(cancellationToken);
        try
        {
            if (_cachedToken is not null && DateTimeOffset.UtcNow < _expiresAt)
                return _cachedToken;

            var client = httpClientFactory.CreateClient(HttpClientName);
            var tokenEndpoint = $"realms/{options.Realm}/protocol/openid-connect/token";

            var requestBody = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = options.AdminClientId,
                ["client_secret"] = options.AdminClientSecret
            };

            using var response = await client.PostAsync(
                tokenEndpoint,
                new FormUrlEncodedContent(requestBody),
                cancellationToken);
            response.EnsureSuccessStatusCode();

            var payload = await response.Content.ReadFromJsonAsync<KeycloakTokenResponse>(cancellationToken)
                ?? throw new InvalidOperationException("Keycloak token response was empty.");

            _cachedToken = payload.AccessToken;
            _expiresAt = DateTimeOffset.UtcNow.AddSeconds(payload.ExpiresIn - 10);

            return _cachedToken;
        }
        finally
        {
            _lock.Release();
        }
    }
}
