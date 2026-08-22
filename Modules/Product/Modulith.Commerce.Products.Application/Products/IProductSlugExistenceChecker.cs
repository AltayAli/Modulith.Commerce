namespace Modulith.Commerce.Products.Application.Products
{
    public interface IProductSlugExistenceChecker
    {
        Task<bool> ExistsAsync(string slug, CancellationToken cancellationToken = default);
    }
}
