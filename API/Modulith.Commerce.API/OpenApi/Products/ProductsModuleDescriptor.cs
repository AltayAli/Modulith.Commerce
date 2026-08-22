using Asp.Versioning;

namespace Modulith.Commerce.API.OpenApi.Products;

public sealed class ProductsModuleDescriptor : IModuleDescriptor
{
    public string Key => "products";
    public string Title => "Products API";
    public IReadOnlyList<ApiVersion> Versions { get; } = [new ApiVersion(1, 0)];
    public string? Description => "Manages products, categories, brands, models and variants.";
}
