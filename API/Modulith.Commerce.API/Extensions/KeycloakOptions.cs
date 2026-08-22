namespace Modulith.Commerce.API.Extensions;

public sealed class KeycloakOptions
{
    public const string SectionName = "Keycloak";

    public required string Authority { get; init; }
    public required string ValidIssuer { get; init; }
    public required string Audience { get; init; }
}
