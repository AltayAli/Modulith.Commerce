namespace Modulith.Commerce.Products.Application.Brands
{
    public interface IBrandExistenceChecker
    {
        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
    }
}
