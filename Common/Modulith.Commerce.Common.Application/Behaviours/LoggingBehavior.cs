using MediatR;
using Microsoft.Extensions.Logging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Serilog.Context;

namespace Modulith.Commerce.Common.Application.Behaviours
{
    public class LoggingBehavior<TRequest, TResponse>
                                    (ILogger<LoggingBehavior<TRequest, TResponse>> _logger)
                                    : IPipelineBehavior<TRequest, TResponse>
                                    where TRequest : IBaseRequest
                                    where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string requestName = GetRequestName(request);
            string moduleName = GetModuleName(typeof(TRequest).FullName);
            using (LogContext.PushProperty("Module", moduleName))
                try
                {
                    _logger.LogInformation("Handling request {RequestName} with content: {@Request}", requestName, request);

                    var result = await next();

                    if (result.IsSuccess)
                    {
                        _logger.LogInformation("Successfully handled request {RequestName} with result: {@Result}", requestName, result);
                    }
                    else
                    {
                        using (LogContext.PushProperty("Error", result.Error, true))
                        {
                            _logger.LogWarning("Handled request {RequestName} with failure: {@Result}", requestName, result);
                        }
                    }

                    return result;
                }
                catch (Exception)
                {
                    _logger.LogError("Exception occurred while handling request {RequestName}", requestName);
                    throw;
                }
                finally
                {
                    _logger.LogInformation("Finished handling request {RequestName}", requestName);
                }
        }

        private string GetRequestName(TRequest request)
        {
            var requestName = request.GetType().Name;

            return " [ " + requestName + " ] ";
        }
        private string GetModuleName(string requestName)
        {
            var moduleName = requestName.Split('.')[1];
            return " [ " + moduleName + " ] ";
        }
    }
}
