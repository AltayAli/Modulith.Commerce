using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record Currency : ValueObject
    {
        public string Code { get; }
        private Currency() { }
        private Currency(string code)
        {
            Code = code;
        }
        public static Currency EUR => new Currency("EUR");
        public static Currency USD => new Currency("USD");
        public static Currency TRY => new Currency("TRY");

        public static IReadOnlyCollection<Currency> All => [EUR, USD, TRY];

        public static Currency GetFromCode(string code)
            => All.FirstOrDefault(x => x.Code == code) ??
               throw new ArgumentException($"Unknown currency code: {code}");
    }
}
