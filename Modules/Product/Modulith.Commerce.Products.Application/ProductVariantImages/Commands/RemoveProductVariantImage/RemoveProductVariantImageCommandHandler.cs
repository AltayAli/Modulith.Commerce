using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariantImages;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariantImages.Commands.RemoveProductVariantImage
{
    public class RemoveProductVariantImageCommandHandler(
        IProductVariantImagesRepository productVariantImagesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<RemoveProductVariantImageCommand>
    {
        public async Task<Result> Handle(RemoveProductVariantImageCommand request, CancellationToken cancellationToken)
        {
            var image = await productVariantImagesRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<ProductVariantImage>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariantImage, bool>>>
                {
                    i => i.Id == request.Id
                }
            });

            if (image is null)
                return Result.Failure(ProductVariantImageErrors.NotFound);

            await productVariantImagesRepository.DeleteAsync(image, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
