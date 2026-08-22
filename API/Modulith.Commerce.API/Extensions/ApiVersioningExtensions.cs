using Asp.Versioning;
using Asp.Versioning.Builder;
using Modulith.Commerce.API.OpenApi;

namespace Modulith.Commerce.API.Extensions;

public static class ApiVersioningExtensions
{
    public static ApiVersionSet CreateModuleVersionSet(
        this IEndpointRouteBuilder app,
        IModuleDescriptor module)
    {
        var builder = app.NewApiVersionSet(module.Key);
        foreach (var version in module.Versions)
            builder = builder.HasApiVersion(version);
        return builder.ReportApiVersions().Build();
    }
}
