using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Models;
using Modulith.Commerce.Products.Domain.ProductCategories;
using Modulith.Commerce.Products.Domain.Products.Events;
using Modulith.Commerce.Products.Domain.ProductVariants;

namespace Modulith.Commerce.Products.Domain.Products
{
    public class Product : BaseEntity
    {
        private Product()
        {
            ProductCategories = new HashSet<ProductCategory>();
            Variants = new HashSet<ProductVariant>();
        }
        public Text Name { get; private set; }
        public Guid? ModelId { get; private set; }
        public Model? Model { get; private set; }
        public string Description { get; private set; }
        public Slug Slug { get; private set; }
        public string? ShortDescription { get; private set; }
        public ProductStatus Status { get; private set; }
        public bool IsFeatured { get; private set; }
        public string TaxClass { get; private set; } = "standard";
        public SeoMetadata? Seo { get; private set; }
        public decimal AvgRating { get; private set; }
        public int ReviewCount { get; private set; }
        public DateTimeOffset? PublishedAt { get; private set; }
        public HashSet<ProductCategory> ProductCategories { get; private set; }
        public HashSet<ProductVariant> Variants { get; private set; }

        public static Product Create(
            string name,
            string description,
            Slug slug,
            Guid? modelId = null,
            string? shortDescription = null,
            bool isFeatured = false,
            string taxClass = "standard",
            SeoMetadata? seo = null)
        {
            var product = new Product
            {
                Name = (Text)name,
                Description = description,
                Slug = slug,
                ModelId = modelId,
                ShortDescription = shortDescription,
                Status = ProductStatus.Draft,
                IsFeatured = isFeatured,
                TaxClass = taxClass,
                Seo = seo,
                AvgRating = 0,
                ReviewCount = 0
            };
            product.AddDomainEvent(new ProductCreateEvent(product.Id));
            return product;
        }

        public Product Update(
            string name,
            string description,
            Slug slug,
            Guid? modelId = null,
            string? shortDescription = null,
            bool isFeatured = false,
            string taxClass = "standard",
            SeoMetadata? seo = null)
        {
            Name = (Text)name;
            Description = description;
            Slug = slug;
            ModelId = modelId;
            ShortDescription = shortDescription;
            IsFeatured = isFeatured;
            TaxClass = taxClass;
            Seo = seo;
            AddDomainEvent(new ProductUpdateEvent(Id));
            return this;
        }

        public Product Publish(DateTime publishedAtUtc)
        {
            if (Status == ProductStatus.Active)
                return this;

            Status = ProductStatus.Active;
            PublishedAt = new DateTimeOffset(publishedAtUtc, TimeSpan.Zero);
            AddDomainEvent(new ProductPublishedEvent(Id));
            return this;
        }

        public Product Unpublish()
        {
            Status = ProductStatus.Inactive;
            AddDomainEvent(new ProductUnpublishedEvent(Id));
            return this;
        }

        public Product Archive()
        {
            Status = ProductStatus.Archived;
            AddDomainEvent(new ProductArchivedEvent(Id));
            return this;
        }
    }
}
