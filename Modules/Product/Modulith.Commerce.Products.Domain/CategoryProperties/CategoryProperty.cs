using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Categories;
using Modulith.Commerce.Products.Domain.CategoryPropertyValues;

namespace Modulith.Commerce.Products.Domain.CategoryProperties
{
    public sealed class CategoryProperty : BaseEntity
    {
        private CategoryProperty()
        {
            Values = new HashSet<CategoryPropertyValue>();
        }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }
        public Text Name { get; private set; }
        public CategoryPropertyType Type { get; private set; }
        public bool AddToFilter { get; private set; }
        public bool IsRequired { get; private set; }
        public int DisplayOrder { get; private set; }
        public HashSet<CategoryPropertyValue> Values { get; private set; }

        public static CategoryProperty Create(
                        Guid categoryId,
                        string name,
                        CategoryPropertyType type,
                        bool addToFilter,
                        bool isRequired,
                        int displayOrder)
        {
            var categoryProperty = new CategoryProperty
            {
                CategoryId = categoryId,
                Name = (Text)name,
                Type = type,
                AddToFilter = addToFilter,
                IsRequired = isRequired,
                DisplayOrder = displayOrder
            };

            return categoryProperty;
        }

        public CategoryProperty Update(
                        string name,
                        CategoryPropertyType type,
                        bool addToFilter,
                        bool isRequired,
                        int displayOrder)
        {
            Name = (Text)name;
            Type = type;
            AddToFilter = addToFilter;
            IsRequired = isRequired;
            DisplayOrder = displayOrder;
            return this;
        }
    }
}
