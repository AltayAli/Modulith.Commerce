namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrand
{
    public class GetBrandResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
