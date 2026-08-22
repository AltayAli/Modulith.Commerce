using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Modulith.Commerce.Common.Infrastructure.Messaging
{
    public static class MessagingConfiguration
    {
        public static IServiceCollection AddModulithCommerceMessaging(this IServiceCollection services, params Assembly[] consumerAssemblies)
        {
            services.AddMassTransit(x =>
            {
                foreach (var assembly in consumerAssemblies)
                {
                    x.AddConsumers(assembly);
                }

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
