using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Modulith.Commerce.Common.Auth;

public sealed class KeycloakClient(
    HttpClient httpClient,
    KeycloakTokenProvider tokenProvider,
    KeycloakClientOptions options) : IKeycloakClient
{
    public async Task<Guid> CreateUserAsync(KeycloakUserCreateRequest request, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var payload = new
        {
            email = request.Email,
            username = request.Email,
            firstName = request.FirstName,
            lastName = request.LastName,
            enabled = request.Enabled,
            credentials = new[]
            {
                new { type = "password", value = request.Password, temporary = request.Temporary }
            }
        };

        var endpoint = $"admin/realms/{options.Realm}/users";
        using var response = await httpClient.PostAsJsonAsync(endpoint, payload, cancellationToken);
        response.EnsureSuccessStatusCode();

        var location = response.Headers.Location
            ?? throw new InvalidOperationException("Keycloak did not return a Location header for the created user.");

        return Guid.Parse(location.Segments[^1]);
    }

    public async Task DeleteUserAsync(Guid keycloakUserId, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var endpoint = $"admin/realms/{options.Realm}/users/{keycloakUserId}";
        using var response = await httpClient.DeleteAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateUserAsync(Guid keycloakUserId, string firstName, string lastName, bool enabled, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var payload = new
        {
            firstName,
            lastName,
            enabled
        };

        var endpoint = $"admin/realms/{options.Realm}/users/{keycloakUserId}";
        using var response = await httpClient.PutAsJsonAsync(endpoint, payload, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task CreateRoleAsync(string keycloakRoleName, string? description, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var payload = new
        {
            name = keycloakRoleName,
            description
        };

        var endpoint = $"admin/realms/{options.Realm}/roles";
        using var response = await httpClient.PostAsJsonAsync(endpoint, payload, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateRoleAsync(string currentKeycloakRoleName, string newKeycloakRoleName, string? description, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var payload = new
        {
            name = newKeycloakRoleName,
            description
        };

        var endpoint = $"admin/realms/{options.Realm}/roles/{currentKeycloakRoleName}";
        using var response = await httpClient.PutAsJsonAsync(endpoint, payload, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteRoleAsync(string keycloakRoleName, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var endpoint = $"admin/realms/{options.Realm}/roles/{keycloakRoleName}";
        using var response = await httpClient.DeleteAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<Guid?> FindUserIdAsync(string email, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var endpoint = $"admin/realms/{options.Realm}/users?email={Uri.EscapeDataString(email)}&exact=true";
        using var response = await httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var users = await response.Content.ReadFromJsonAsync<List<KeycloakUserSummary>>(cancellationToken);
        var match = users?.FirstOrDefault();

        return match is null ? null : Guid.Parse(match.Id);
    }

    public async Task<bool> RoleExistsAsync(string keycloakRoleName, CancellationToken cancellationToken)
    {
        await AuthorizeAsync(cancellationToken);

        var endpoint = $"admin/realms/{options.Realm}/roles/{Uri.EscapeDataString(keycloakRoleName)}";
        using var response = await httpClient.GetAsync(endpoint, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return false;

        response.EnsureSuccessStatusCode();
        return true;
    }

    private async Task AuthorizeAsync(CancellationToken cancellationToken)
    {
        var token = await tokenProvider.GetAdminAccessTokenAsync(cancellationToken);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
