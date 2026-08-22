namespace Modulith.Commerce.Common.Auth;

public sealed class KeycloakClientOptions
{
    public const string SectionName = "Keycloak";

    public required string BaseUrl { get; init; }
    public required string Realm { get; init; }
    public required string AdminClientId { get; init; }
    public required string AdminClientSecret { get; init; }
}
