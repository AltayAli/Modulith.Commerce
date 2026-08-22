namespace Modulith.Commerce.Common.Auth;

public sealed record KeycloakUserCreateRequest(
    string Email,
    string FirstName,
    string LastName,
    string Password,
    bool Enabled = true,
    bool Temporary = true);
