using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record Text : ValueObject
    {
        public string Value { get; }
        public Text(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.", nameof(value));
            Value = value;
        }

        public static implicit operator string(Text n) => n?.Value;
        public static explicit operator Text(string n) => new Text(n);
        public override string ToString() => Value;
        public string ToNormalizedString() => Value.Trim().ToLower();
    }
}
