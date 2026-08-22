namespace Modulith.Commerce.Common.Domain.Abstractions
{
    public interface IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<IQueryable<TEntity>> SelectAsync(FilteringOptions<TEntity> listingOptions = null, CancellationToken cancellationToken = default);
        Task<TEntity?> SelectSimpleOrDefaultAsync(FilteringOptions<TEntity> listingOptions, CancellationToken cancellationToken = default);
        Task<TEntity?> InsertAsync(TEntity model, CancellationToken cancellationToken = default);
        Task<TEntity?> UpdateAsync(TEntity model, CancellationToken cancellationToken = default);
        Task<TEntity?> DeleteAsync(TEntity model, CancellationToken cancellationToken = default);
    }
}
