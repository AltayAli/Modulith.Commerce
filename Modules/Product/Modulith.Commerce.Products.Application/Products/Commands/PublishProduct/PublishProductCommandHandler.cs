using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Commands.PublishProduct
{
    public class PublishProductCommandHandler(
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
        : ICommandHandler<PublishProductCommand>
    {
        public async Task<Result> Handle(PublishProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                IsLoadingAsNoTracking = false,
                Relations = new List<string> { "Variants" },
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                }
            }, cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.NotFound);

            if (!product.Variants.Any(v => v.IsActive))
                return Result.Failure(ProductErrors.CannotPublishWithoutActiveVariant);

            product.Publish(dateTimeProvider.UtcNow);
            await productsRepository.UpdateAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
