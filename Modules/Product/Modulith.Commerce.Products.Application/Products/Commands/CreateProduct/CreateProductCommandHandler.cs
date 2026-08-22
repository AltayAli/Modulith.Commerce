using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Models;
using Modulith.Commerce.Products.Domain.ProductCategories;
using Modulith.Commerce.Products.Domain.Products;
using System.Linq.Expressions;

namespace Modulith.Commerce.Products.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IModelsRepository modelsRepository,
        IProductsRepository productsRepository,
        IProductCategoriesRepository productCategoriesRepository,
        IProductSlugExistenceChecker productSlugExistenceChecker,
        IUnitOfWork unitOfWork)
        : ICommandHandler<CreateProductCommand, Guid>
    {
        private const int MaxSlugSuffixAttempts = 50;
        private const int SlugSuffixLength = 6;
        private const string SlugSuffixAlphabet = "abcdefghijklmnopqrstuvwxyz0123456789";

        public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
                    return Result.Failure<Guid>(default, ModelErrors.NotFound);
            }

            var slugResult = await ResolveSlugAsync(request, cancellationToken);
            if (slugResult.IsFailure)
                return Result.Failure<Guid>(default, slugResult.Error);

            var seo = request.Seo is null
                ? null
                : new SeoMetadata
                {
                    Title = request.Seo.Title,
                    Description = request.Seo.Description,
                    Keywords = request.Seo.Keywords,
                    OgImage = request.Seo.OgImage
                };

            var product = Product.Create(
                request.Name,
                request.Description,
                slugResult.Value,
                request.ModelId,
                request.ShortDescription,
                request.IsFeatured,
                request.TaxClass,
                seo);

            await productsRepository.InsertAsync(product, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                var productCategory = ProductCategory.Create(product.Id, categoryId);
                await productCategoriesRepository.InsertAsync(productCategory, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(product.Id);
        }

        private async Task<Result<Slug>> ResolveSlugAsync(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.Slug))
            {
                var manualSlug = new Slug(request.Slug);
                bool manualSlugExists = await productSlugExistenceChecker.ExistsAsync(manualSlug.Value, cancellationToken);

                return manualSlugExists
                    ? Result.Failure<Slug>(null, ProductErrors.SlugAlreadyExists)
                    : Result.Success(manualSlug);
            }

            var baseSlug = Slug.GenerateFrom(request.Name);
            bool baseSlugExists = await productSlugExistenceChecker.ExistsAsync(baseSlug.Value, cancellationToken);

            if (!baseSlugExists)
                return Result.Success(baseSlug);

            for (int attempt = 0; attempt < MaxSlugSuffixAttempts; attempt++)
            {
                var candidate = new Slug($"{baseSlug.Value}-{GenerateRandomSuffix()}");
                bool candidateExists = await productSlugExistenceChecker.ExistsAsync(candidate.Value, cancellationToken);

                if (!candidateExists)
                    return Result.Success(candidate);
            }

            return Result.Failure<Slug>(null, ProductErrors.SlugAlreadyExists);
        }

        private static string GenerateRandomSuffix()
        {
            Span<char> buffer = stackalloc char[SlugSuffixLength];

            for (int i = 0; i < SlugSuffixLength; i++)
                buffer[i] = SlugSuffixAlphabet[Random.Shared.Next(SlugSuffixAlphabet.Length)];

            return new string(buffer);
        }
    }
}
