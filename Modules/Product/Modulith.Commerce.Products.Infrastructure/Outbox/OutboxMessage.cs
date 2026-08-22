namespace Modulith.Commerce.Products.Infrastructure.Outbox
{
    public class OutboxMessage
    {
        public OutboxMessage()
        {

        }
        public OutboxMessage(string content,
            string type,
            DateTime occuredOnUtc)
        {
            Content = content;
            Type = type;
            Occured = occuredOnUtc;
        }
        public Guid Id { get; init; }
        public DateTime Occured { get; init; }
        public string Content { get; init; }
        public string Type { get; init; }
        public DateTime? Processed { get; set; }
        public string? Error { get; set; }
    }
}
