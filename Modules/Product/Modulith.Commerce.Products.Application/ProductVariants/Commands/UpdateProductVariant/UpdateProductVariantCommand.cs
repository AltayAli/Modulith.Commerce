using Modulith.Commerce.Common.Application.Abstractions.Messaging;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.UpdateProductVariant
{
    public record UpdateProductVariantCommand : ICommand
    {
        public Guid VariantId { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public decimal? CostAmount { get; set; }
        public string? CostCurrency { get; set; }
        public decimal? TaxRate { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DiscountCurrency { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public List<UpdateProductVariantImageItem> Images { get; set; } = new();
        public List<UpdateProductVariantPropertyItem> Properties { get; set; } = new();
    }

    public record UpdateProductVariantImageItem
    {
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }

    public record UpdateProductVariantPropertyItem
    {
        public Guid CategoryPropertyId { get; set; }
        public string Value { get; set; }
    }
}
