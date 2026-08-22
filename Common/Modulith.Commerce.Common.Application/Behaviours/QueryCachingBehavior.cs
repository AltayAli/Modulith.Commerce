using MediatR;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Application.Caching;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Application.Behaviours
{
    public class QueryCachingBehavior<TRequest, TResponse>
                                    (ICacheService _cacheService)
                                     : IPipelineBehavior<TRequest, TResponse>
                                     where TRequest : ICacheQuery<TResponse>
                                     where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheResult = await _cacheService.GetAsync<TResponse>(request.CacheKey);

            if (cacheResult is not null)
            {
                return cacheResult;
            }
            else
            {
                var response = await next();
                if (response.IsSuccess)
                {
                    await _cacheService.SetAsync(request.CacheKey, response, request.Expiration);
                }
                return response;
            }
        }
    }
}
