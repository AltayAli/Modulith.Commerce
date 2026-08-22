using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Common.Domain.ValueObjects;
using Modulith.Commerce.Products.Domain.Categories.Events;
using Modulith.Commerce.Products.Domain.CategoryProperties;

namespace Modulith.Commerce.Products.Domain.Categories
{
    public sealed class Category : BaseEntity
    {
        private Category()
        {
            Properties = new HashSet<CategoryProperty>();
            Children = new HashSet<Category>();
        }
        public Text Name { get; private set; }
        public Guid? ParentId { get; private set; }
        public Category Parent { get; private set; }
        public Icon? Icon { get; private set; }
        public HashSet<Category> Children { get; private set; }
        public HashSet<CategoryProperty> Properties { get; private set; }

        public static Category Create(string name, string icon, Guid? parentId = null)
        {
            var category = new Category
            {
                Name = (Text)name,
                ParentId = parentId,
                Icon = CreateIconInstance(icon)
            };

            category.AddDomainEvent(new AddCategoryEvent());

            return category;
        }

        public Category Update(string name, string icon, Guid? parentId = null)
        {
            Name = (Text)name;
            ParentId = parentId;
            Icon = CreateIconInstance(icon);

            AddDomainEvent(new UpdateCategoryEvent());
            return this;
        }

        public void Remove()
        {
            AddDomainEvent(new RemoveCategoryEvent());
        }

        private static Icon? CreateIconInstance(string icon)
        {
            Icon? createdIcon = null;

            if (!string.IsNullOrWhiteSpace(icon))
            {
                createdIcon = new Icon(icon);
            }

            return createdIcon;
        }
    }
}
