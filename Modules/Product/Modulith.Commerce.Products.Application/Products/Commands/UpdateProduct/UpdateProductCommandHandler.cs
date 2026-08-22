using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Models;
using Modulith.Commerce.Products.Domain.ProductCategories;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IProductsRepository productsRepository,
        IModelsRepository modelsRepository,
        IProductCategoriesRepository productCategoriesRepository,
        IProductSlugExistenceChecker productSlugExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateProductCommand>
    {
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ModelId.HasValue)
            {
                bool modelExists = await modelsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Model>
                {
                    Predicates = new List<Expression<Func<Model, bool>>>
                    {
                        m => m.Id == request.ModelId.Value
                    }
                }) is not null;

                if (!modelExists)
                    return Result.Failure(ModelErrors.NotFound);
            }

            var product = await productsRepository.SelectSimpleOrDefaultAsync(new FilteringOptions<Product>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<Product, bool>>>
                {
                    p => p.Id == request.Id
                }
            });

            if (product is null)
                return Result.Failure(ProductErrors.NotFound);

            var slug = product.Slug;
            if (!string.IsNullOrWhiteSpace(request.Slug))
            {
                var requestedSlug = new Slug(request.Slug);
                if (requestedSlug.Value != product.Slug.Value)
                {
                    bool slugExists = await productSlugExistenceChecker.ExistsAsync(requestedSlug.Value, cancellationToken);
                    if (slugExists)
                        return Result.Failure(ProductErrors.SlugAlreadyExists);
                }

                slug = requestedSlug;
            }

            var seo = request.Seo is null
                ? null
                : new SeoMetadata
                {
                    Title = request.Seo.Title,
                    Description = request.Seo.Description,
                    Keywords = request.Seo.Keywords,
                    OgImage = request.Seo.OgImage
                };

            product.Update(
                request.Name,
                request.Description,
                slug,
                request.ModelId,
                request.ShortDescription,
                request.IsFeatured,
                request.TaxClass,
                seo);

            await productsRepository.UpdateAsync(product, cancellationToken);

            var existingCategories = (await productCategoriesRepository.SelectAsync(new FilteringOptions<ProductCategory>
            {
                IsLoadingAsNoTracking = false,
                Predicates = new List<Expression<Func<ProductCategory, bool>>>
                {
                    pc => pc.ProductId == request.Id
                }
            })).ToList();

            foreach (var existing in existingCategories)
                await productCategoriesRepository.DeleteAsync(existing, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                var productCategory = ProductCategory.Create(product.Id, categoryId);
                await productCategoriesRepository.InsertAsync(productCategory, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
