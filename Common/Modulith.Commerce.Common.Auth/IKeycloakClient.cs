namespace Modulith.Commerce.Common.Auth;

public interface IKeycloakClient
{
    Task<Guid> CreateUserAsync(KeycloakUserCreateRequest request, CancellationToken cancellationToken);

    Task DeleteUserAsync(Guid keycloakUserId, CancellationToken cancellationToken);

    Task UpdateUserAsync(Guid keycloakUserId, string firstName, string lastName, bool enabled, CancellationToken cancellationToken);

    Task CreateRoleAsync(string keycloakRoleName, string? description, CancellationToken cancellationToken);

    Task UpdateRoleAsync(string currentKeycloakRoleName, string newKeycloakRoleName, string? description, CancellationToken cancellationToken);

    Task DeleteRoleAsync(string keycloakRoleName, CancellationToken cancellationToken);

    Task<Guid?> FindUserIdAsync(string email, CancellationToken cancellationToken);

    Task<bool> RoleExistsAsync(string keycloakRoleName, CancellationToken cancellationToken);
}
