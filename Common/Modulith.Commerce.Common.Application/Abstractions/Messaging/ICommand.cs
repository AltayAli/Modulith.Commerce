using MediatR;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }
}
