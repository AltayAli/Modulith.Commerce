using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record PhoneNumberCountryCode : ValueObject
    {
        private PhoneNumberCountryCode() { }
        public PhoneNumberCountryCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Country code cannot be null or empty.", nameof(code));
            Code = code;
        }
        public string Code { get; private set; }

        public static PhoneNumberCountryCode TR => new("+90");
        public static PhoneNumberCountryCode AZE => new("+994");

        public static IReadOnlyCollection<PhoneNumberCountryCode> All => [TR, AZE];
        public static implicit operator PhoneNumberCountryCode(string countryCode) => new PhoneNumberCountryCode(countryCode);
        public static implicit operator string(PhoneNumberCountryCode countryCode) => countryCode.Code;
    }
}
