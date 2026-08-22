
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Categories
{
    public static class CategoryErrors
    {
        public static Error NullValue => new Error("Caregory.NullValue", "Caregory.NullValue");
        public static Error NotFound => new Error("Caregory.NotFound", "Caregory.NotFound");
        public static Error MaxLenght => new Error("Caregory.MaxLenght", "Caregory.MaxLenght");
        public static Error AlreadyExists => new Error("Caregory.AlreadyExists", "Caregory.AlreadyExists");
        public static Error ParentCategoryNotFound => new Error("Caregory.ParentCategoryNotFound", "Caregory.ParentCategoryNotFound");
    }
}
