namespace Modulith.Commerce.API.Extensions;

public sealed class KeycloakWebClientOptions
{
    public const string SectionName = "Keycloak:Web";

    public required string ClientId { get; init; }
    public required string ClientSecret { get; init; }
    public required string CallbackPath { get; init; }
    public required string SignedOutCallbackPath { get; init; }
}
