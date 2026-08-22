using MediatR;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
