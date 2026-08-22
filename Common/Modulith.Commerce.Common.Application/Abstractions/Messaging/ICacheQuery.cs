namespace Modulith.Commerce.Common.Application.Abstractions.Messaging
{
    public interface ICacheQuery<TResponse> : IQuery<TResponse>, ICacheQuery;
    public interface ICacheQuery
    {
        string CacheKey { get; }
        TimeSpan? Expiration { get; }
    }
}
