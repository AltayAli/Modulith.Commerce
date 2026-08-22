using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.ProductVariants
{
    public record Barcode : ValueObject
    {
        public string Value { get; }

        public Barcode(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Barcode must not be empty.", nameof(value));
            if (value.Length > 64) throw new ArgumentException("Barcode too long.", nameof(value));

            Value = value;
        }

        public static implicit operator string(Barcode b) => b?.Value;
        public static explicit operator Barcode(string s) => new Barcode(s);

        public override string ToString() => Value;
    }
}
