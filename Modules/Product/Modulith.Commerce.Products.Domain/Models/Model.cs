using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Models.Events;

namespace Modulith.Commerce.Products.Domain.Models
{
    public class Model : BaseEntity
    {
        private Model()
        {
            Products = new HashSet<Products.Product>();
        }
        public Text Name { get; private set; }
        public Guid BrandId { get; private set; }
        public Brands.Brand Brand { get; private set; }
        public HashSet<Products.Product> Products { get; private set; }

        public static Model Create(string name, Guid brandId)
        {
            var model = new Model
            {
                Name = (Text)name,
                BrandId = brandId
            };
            model.AddDomainEvent(new AddModelEvent());
            return model;
        }

        public Model Update(string name, Guid brandId)
        {
            Name = (Text)name;
            BrandId = brandId;
            AddDomainEvent(new UpdateModelEvent());
            return this;
        }
    }
}
