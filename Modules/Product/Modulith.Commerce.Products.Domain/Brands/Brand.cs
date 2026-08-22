using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Brands.Events;

namespace Modulith.Commerce.Products.Domain.Brands
{
    public class Brand : BaseEntity
    {
        public Brand()
        {
            Models = new HashSet<Models.Model>();
        }
        public Text Name { get; private set; }
        public HashSet<Models.Model> Models { get; private set; }

        public static Brand Create(string name)
        {
            var brand = new Brand
            {
                Name = (Text)name
            };
            brand.AddDomainEvent(new AddBrandEvent(brand));
            return brand;
        }

        public Brand Update(string name)
        {
            Name = (Text)name;
            AddDomainEvent(new UpdateBrandEvent(Id));
            return this;
        }

        public void Remove()
        {
            AddDomainEvent(new RemoveBrandEvent(Id));
        }
    }
}
