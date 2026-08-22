using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record Email : ValueObject
    {
        public string Value { get; }
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email cannot be null or empty.", nameof(value));
            Value = value;
        }

        public static implicit operator string(Email n) => n?.Value;
        public static explicit operator Email(string n) => new Email(n);
        public override string ToString() => Value;
        public string ToNormalizedString() => Value.Trim().ToLower();
    }
}
