using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariantProperties.Commands.RemoveProductVariantProperty
{
    public class RemoveProductVariantPropertyCommandHandler(
        IProductVariantPropertiesRepository productVariantPropertiesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveProductVariantPropertyCommand>
    {
        public async Task<Result> Handle(RemoveProductVariantPropertyCommand request, CancellationToken cancellationToken)
        {
            var property = await productVariantPropertiesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<ProductVariantProperty>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariantProperty, bool>>>
                {
                    p => p.Id == request.Id
                }
            });

            if (property is null)
                return Result.Failure(ProductVariantPropertyErrors.NotFound);

            await productVariantPropertiesRepository.DeleteAsync(property, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
