namespace Modulith.Commerce.Products.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesItemResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Icon { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int SubCategoriesCount { get; set; }
    }
}
