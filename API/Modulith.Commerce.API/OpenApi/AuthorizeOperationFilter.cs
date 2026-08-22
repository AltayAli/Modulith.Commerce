using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Modulith.Commerce.API.OpenApi;

public sealed class AuthorizeOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var metadata = context.ApiDescription.ActionDescriptor.EndpointMetadata;

        var isAnonymous = metadata.OfType<IAllowAnonymous>().Any();
        var authorizeData = metadata.OfType<IAuthorizeData>().ToList();

        if (isAnonymous || authorizeData.Count == 0)
            return;

        var policies = authorizeData
            .Select(a => a.Policy)
            .Where(policy => !string.IsNullOrWhiteSpace(policy))
            .Distinct()
            .ToList();

        var authNote = policies.Count > 0
            ? $"**Yetki:** `{string.Join("`, `", policies)}`"
            : "**Yetki:** Kimlik doğrulama gerekli (policy yok).";

        operation.Description = string.IsNullOrWhiteSpace(operation.Description)
            ? authNote
            : $"{operation.Description}\n\n{authNote}";

        operation.Responses ??= new OpenApiResponses();
        operation.Responses["401"] = new OpenApiResponse { Description = "Kimlik doğrulanmadı." };
        operation.Responses["403"] = new OpenApiResponse { Description = "Yetki yok." };
    }
}
