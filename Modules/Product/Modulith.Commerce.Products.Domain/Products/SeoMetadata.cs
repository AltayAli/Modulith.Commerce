namespace Modulith.Commerce.Products.Domain.Products
{
    public record SeoMetadata
    {
        public string? Title { get; init; }
        public string? Description { get; init; }
        public List<string> Keywords { get; init; } = new();
        public string? OgImage { get; init; }
    }
}
