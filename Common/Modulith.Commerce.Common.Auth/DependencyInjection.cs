using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Commerce.Common.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddKeycloakClient(this IServiceCollection services, IConfiguration configuration)
    {
        if (services.Any(d => d.ServiceType == typeof(IKeycloakClient)))
        {
            return services;
        }

        var options = configuration
            .GetSection(KeycloakClientOptions.SectionName)
            .Get<KeycloakClientOptions>()
            ?? throw new InvalidOperationException("Keycloak configuration section is missing.");

        services.AddSingleton(options);

        services.AddHttpClient(KeycloakTokenProvider.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });
        services.AddSingleton<KeycloakTokenProvider>();

        services.AddHttpClient<IKeycloakClient, KeycloakClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }
}
