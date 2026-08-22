using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Application.Behaviours;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Common.Infrastructure.Caching;
using Modulith.Commerce.Common.Infrastructure.Clock;
using System.Reflection;

namespace Modulith.Commerce.Common.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static void AddApplicationConfiguration(this IServiceCollection services,
                                                IConfiguration configuration,
                                                Assembly[] assemblies)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assemblies);

                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ExceptionHandlingBehavior<,>));
            });

            services.AddValidatorsFromAssemblies(assemblies);

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICacheService, CacheService>();

            AddCahce(services, configuration);

            services.AddHttpContextAccessor();
        }
        private static IServiceCollection AddCahce(IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("CacheConnection");

            services.AddStackExchangeRedisCache(options => { options.Configuration = connectionString; });

            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }
    }
}
