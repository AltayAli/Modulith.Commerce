using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Products;

namespace Modulith.Commerce.Products.Domain.ProductCategories
{
    public class ProductCategory : BaseEntity
    {
        private ProductCategory()
        {
        }

        public Guid ProductId { get; private set; }
        public Product Product { get; private set; }
        public Guid CategoryId { get; private set; }
        public Categories.Category Category { get; private set; }

        public static ProductCategory Create(Guid productId, Guid categoryId)
        {
            return new ProductCategory
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CategoryId = categoryId
            };
        }
    }
}
