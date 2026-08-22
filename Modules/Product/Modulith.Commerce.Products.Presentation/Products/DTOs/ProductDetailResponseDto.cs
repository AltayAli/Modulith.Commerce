namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record ProductDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string? ShortDescription { get; set; }
        public Guid? ModelId { get; set; }
        public string? ModelName { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public string TaxClass { get; set; }
        public ProductSeoItemDto? Seo { get; set; }
        public decimal AvgRating { get; set; }
        public int ReviewCount { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<ProductCategoryItemDto> Categories { get; set; } = new();
        public List<ProductVariantItemDto> Variants { get; set; } = new();
    }

    public record ProductSeoItemDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string> Keywords { get; set; } = new();
        public string? OgImage { get; set; }
    }

    public record ProductCategoryItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public record ProductVariantItemDto
    {
        public Guid Id { get; set; }
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
        public List<ProductVariantImageItemDto> Images { get; set; } = new();
        public List<ProductVariantPropertyItemDto> Properties { get; set; } = new();
    }

    public record ProductVariantImageItemDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }

    public record ProductVariantPropertyItemDto
    {
        public Guid Id { get; set; }
        public Guid CategoryPropertyId { get; set; }
        public string CategoryPropertyName { get; set; }
        public string Value { get; set; }
    }
}
