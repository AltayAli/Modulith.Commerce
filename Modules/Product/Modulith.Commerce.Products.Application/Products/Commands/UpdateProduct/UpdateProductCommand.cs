using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Products.Application.Products.Commands.CreateProduct;

namespace Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand : ICommand
    {
        public Guid Id { get; set; }
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
}
