using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record Money : ValueObject
    {
        public decimal Amount { get; }
        public Currency Currency { get; }
        public Money(decimal amount, Currency currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public static Money operator +(Money first, Money second)
        {
            if (first.Currency != second.Currency)
            {
                throw new InvalidOperationException("Both currencies are not equal");
            }

            return new Money(first.Amount + second.Amount, first.Currency);
        }

        public bool IsZero => Amount <= 0;
    }
}
