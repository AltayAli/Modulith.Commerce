namespace Modulith.Commerce.Common.Domain.Abstractions
{
    public record Error(string Code, string Name)
    {
        public static Error None => new Error("Error.None", "Unknown error");
        public static Error NullValue => new Error("Error.NullValue", "Null value was provided");
    }
}
