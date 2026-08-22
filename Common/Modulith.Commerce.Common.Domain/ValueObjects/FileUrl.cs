using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Common.Domain.ValueObjects
{
    public record FileUrl : ValueObject
    {
        public FileUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("File URL cannot be null or empty.", nameof(url));
            Url = url;
        }
        public string Url { get; set; }
    }
}
