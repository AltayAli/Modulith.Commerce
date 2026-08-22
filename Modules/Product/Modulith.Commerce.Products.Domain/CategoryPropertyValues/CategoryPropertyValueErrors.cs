using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.CategoryPropertyValues
{
    public class CategoryPropertyValueErrors
    {
        public static Error NullValue => new Error("CategoryPropertyValue.NullValue", "CategoryPropertyValue.NullValue");
        public static Error NotFound => new Error("CategoryPropertyValue.NotFound", "CategoryPropertyValue.NotFound");
        public static Error MaxLenght => new Error("CategoryPropertyValue.MaxLenght", "CategoryPropertyValue.MaxLenght");
        public static Error AlreadyExists => new Error("CategoryPropertyValue.AlreadyExists", "CategoryPropertyValue.AlreadyExists");
        public static Error EmptyItems => new Error("CategoryPropertyValue.EmptyItems", "CategoryPropertyValue.EmptyItems");
    }
}
