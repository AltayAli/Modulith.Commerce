using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Modulith.Commerce.API.OpenApi;

public sealed class ConfigureSwaggerOptions(IReadOnlyList<IModuleDescriptor> modules)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var module in modules)
        {
            foreach (var version in module.Versions)
            {
                var groupName = module.GroupName(version);
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = module.Title,
                    Version = $"v{version.MajorVersion}",
                    Description = module.Description,
                });
            }
        }

        options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);

        options.OperationFilter<AuthorizeOperationFilter>();
    }
}
