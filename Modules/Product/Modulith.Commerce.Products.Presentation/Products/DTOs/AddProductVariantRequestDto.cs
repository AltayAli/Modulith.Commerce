namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record AddProductVariantRequestDto
    {
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
        public List<AddProductVariantImageItemDto> Images { get; set; } = new();
        public List<AddProductVariantPropertyItemDto> Properties { get; set; } = new();
    }
}
