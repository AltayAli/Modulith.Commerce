using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record PhoneNumber : ValueObject
    {
        private PhoneNumber() { }
        public PhoneNumber(string number, string code)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(number));

            Number = number;
            CountryCode = new PhoneNumberCountryCode(code);
        }
        public PhoneNumberCountryCode CountryCode { get; private set; }
        public string Number { get; private set; }
        public static explicit operator PhoneNumber(string number) => new PhoneNumber(number, PhoneNumberCountryCode.TR);
        public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.CountryCode + phoneNumber.Number;
    }
}
