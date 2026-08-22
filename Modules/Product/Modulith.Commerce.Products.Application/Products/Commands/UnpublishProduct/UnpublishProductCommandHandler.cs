using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Commands.UnpublishProduct
{
    public class UnpublishProductCommandHandler(
        IProductsRepository productsRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UnpublishProductCommand>
    {
        public async Task<Result> Handle(UnpublishProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                }
            }, cancellationToken);

            if (product is null)
                return Result.Failure(ProductErrors.NotFound);

            product.Unpublish();
            await productsRepository.UpdateAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
