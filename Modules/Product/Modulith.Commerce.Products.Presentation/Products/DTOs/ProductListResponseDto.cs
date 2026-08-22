namespace Modulith.Commerce.Products.Presentation.Products.DTOs
{
    public record ProductListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string? ShortDescription { get; set; }
        public string? ModelName { get; set; }
        public string Status { get; set; }
        public bool IsFeatured { get; set; }
        public string TaxClass { get; set; }
        public decimal AvgRating { get; set; }
        public int ReviewCount { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public int VariantsCount { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
