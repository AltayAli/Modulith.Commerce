using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.ProductVariants
{
    public record Sku : ValueObject
    {
        public string Value { get; }

        public Sku(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("SKU must not be empty.", nameof(value));
            if (value.Length > 64) throw new ArgumentException("SKU too long.", nameof(value));

            Value = value;
        }

        public static implicit operator string(Sku s) => s?.Value;
        public static explicit operator Sku(string s) => new Sku(s);

        public override string ToString() => Value;
    }
}
