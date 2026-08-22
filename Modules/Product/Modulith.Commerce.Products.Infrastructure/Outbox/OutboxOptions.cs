namespace Modulith.Commerce.Products.Infrastructure.Outbox
{
    public class OutboxOptions
    {
        public int IntervalInSeconds { get; set; }
        public int BatchSize { get; set; }
    }
}
