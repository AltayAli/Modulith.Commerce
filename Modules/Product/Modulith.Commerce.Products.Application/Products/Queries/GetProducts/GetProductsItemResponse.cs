namespace Modulith.Commerce.Products.Application.Products.Queries.GetProducts
{
    public record GetProductsItemResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? ModelName { get; set; }
        public required string Status { get; set; }
        public bool IsFeatured { get; set; }
        public required string TaxClass { get; set; }
        public decimal AvgRating { get; set; }
        public int ReviewCount { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public int VariantsCount { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
