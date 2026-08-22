namespace Modulith.Commerce.Products.Application.Brands.Queries.GetBrands
{
    public class GetBrandsResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
