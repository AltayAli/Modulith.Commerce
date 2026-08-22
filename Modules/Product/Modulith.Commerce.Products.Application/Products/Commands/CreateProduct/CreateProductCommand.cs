using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : ICommand<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Slug { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ModelId { get; set; }
        public bool IsFeatured { get; set; }
        public string TaxClass { get; set; } = "standard";
        public SeoRequest? Seo { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
    }

    public record SeoRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string> Keywords { get; set; } = new();
        public string? OgImage { get; set; }
    }
}
