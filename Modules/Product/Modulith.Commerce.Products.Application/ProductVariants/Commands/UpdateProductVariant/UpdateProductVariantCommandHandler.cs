using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.ProductVariantImages;
using Modulith.Commerce.Products.Domain.ProductVariantProperties;
using Modulith.Commerce.Products.Domain.ProductVariants;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.ProductVariants.Commands.UpdateProductVariant
{
    public class UpdateProductVariantCommandHandler(
        IProductVariantsRepository productVariantsRepository,
        IProductVariantImagesRepository productVariantImagesRepository,
        IProductVariantPropertiesRepository productVariantPropertiesRepository,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateProductVariantCommand>
    {
        public async Task<Result> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {
            var variant = await productVariantsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<ProductVariant>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariant, bool>>>
                {
                    v => v.Id == request.VariantId
                }
            });

            if (variant is null)
                return Result.Failure(ProductVariantErrors.NotFound);

            var price = new Money(request.PriceAmount, Currency.GetFromCode(request.PriceCurrency));

            Money? cost = request.CostAmount.HasValue && request.CostCurrency is not null
                ? new Money(request.CostAmount.Value, Currency.GetFromCode(request.CostCurrency))
                : null;

            var discount = new Money(request.DiscountAmount, Currency.GetFromCode(request.DiscountCurrency));

            variant.Update(
                request.Sku,
                request.Barcode,
                price,
                cost,
                request.TaxRate,
                request.StockQuantity,
                request.IsDefault,
                request.IsActive,
                discount,
                request.DiscountStartDate,
                request.DiscountEndDate);

            await productVariantsRepository.UpdateAsync(variant, cancellationToken);

            var existingImages = (await productVariantImagesRepository.SelectAsync(new FilteringOptions<ProductVariantImage>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariantImage, bool>>>
                {
                    i => i.ProductVariantId == request.VariantId
                }
            })).ToList();

            foreach (var image in existingImages)
                await productVariantImagesRepository.DeleteAsync(image, cancellationToken);

            foreach (var item in request.Images)
            {
                var image = ProductVariantImage.Create(request.VariantId, item.ImageUrl, item.IsMain);
                await productVariantImagesRepository.InsertAsync(image, cancellationToken);
            }

            var existingProperties = (await productVariantPropertiesRepository.SelectAsync(new FilteringOptions<ProductVariantProperty>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductVariantProperty, bool>>>
                {
                    p => p.ProductVariantId == request.VariantId
                }
            })).ToList();

            foreach (var property in existingProperties)
                await productVariantPropertiesRepository.DeleteAsync(property, cancellationToken);

            foreach (var item in request.Properties)
            {
                var property = ProductVariantProperty.Create(request.VariantId, item.CategoryPropertyId, item.Value);
                await productVariantPropertiesRepository.InsertAsync(property, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
