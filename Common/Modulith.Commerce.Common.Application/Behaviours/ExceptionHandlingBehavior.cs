using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Application.Behaviours
{
    public class ExceptionHandlingBehavior<TRequest, TResponse>
                                    (ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
                                    : IPipelineBehavior<TRequest, TResponse>
                                    where TRequest : IBaseRequest
                                    where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "An unhandled exception has occurred while executing the request {RequestName}.", requestName);

                throw new ApplicationException(requestName, innerException: ex);
            }
        }
    }
}
