using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUser.Domain.Permissions
{
    public record Resource : ValueObject
    {
        public Resource(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Resource cannot be null or empty.", nameof(value));
            Value = value;
        }
        public string Value { get; }

        public static implicit operator string(Resource n) => n?.Value;
        public static explicit operator Resource(string n) => new Resource(n);
    }
}
