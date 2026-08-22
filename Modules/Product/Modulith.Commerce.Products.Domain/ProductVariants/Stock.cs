using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.ProductVariants
{
    public record Stock : ValueObject
    {
        public int Quantity { get; private set; }
        public Stock(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Stock quantity cannot be negative.", nameof(quantity));
            Quantity = quantity;
        }
        public void Increase(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Increase amount cannot be negative.", nameof(amount));
            Quantity += amount;
        }
        public void Decrease(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Decrease amount cannot be negative.", nameof(amount));
            if (amount > Quantity)
                throw new InvalidOperationException("Cannot decrease stock below zero.");
            Quantity -= amount;
        }
    }
}
