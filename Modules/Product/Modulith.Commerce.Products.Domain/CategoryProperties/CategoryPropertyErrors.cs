using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.CategoryProperties
{
    public static class CategoryPropertyErrors
    {
        public static Error NullValue => new Error("CategoryProperty.NullValue", "CategoryProperty.NullValue");
        public static Error NotFound => new Error("CategoryProperty.NotFound", "CategoryProperty.NotFound");
        public static Error MaxLenght => new Error("CategoryProperty.MaxLenght", "CategoryProperty.MaxLenght");
        public static Error AlreadyExists => new Error("CategoryProperty.AlreadyExists", "CategoryProperty.AlreadyExists");
        public static Error WrongType => new Error("CategoryProperty.WrongType", "CategoryProperty.WrongType");
        public static Error WrongDisplayOrder => new Error("CategoryProperty.WrongDisplayOrder", "CategoryProperty.WrongDisplayOrder");
        public static Error EmptyItems => new Error("CategoryProperty.EmptyItems", "CategoryProperty.EmptyItems");
    }
}
