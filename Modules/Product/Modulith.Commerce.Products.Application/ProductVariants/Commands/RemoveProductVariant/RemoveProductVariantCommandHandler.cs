using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariants;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.RemoveProductVariant
{
    public class RemoveProductVariantCommandHandler(
        IProductVariantsRepository productVariantsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveProductVariantCommand>
    {
        public async Task<Result> Handle(RemoveProductVariantCommand request, CancellationToken cancellationToken)
        {
            var variant = await productVariantsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<ProductVariant>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariant, bool>>>
                {
                    v => v.Id == request.Id
                }
            });

            if (variant is null)
                return Result.Failure(ProductVariantErrors.NotFound);

            await productVariantsRepository.DeleteAsync(variant, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
