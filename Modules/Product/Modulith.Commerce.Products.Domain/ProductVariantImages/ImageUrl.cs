using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.ProductVariantImages
{
    public record ImageUrl : ValueObject
    {
        public string Url { get; }
        protected ImageUrl() { }

        public ImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentException("Image URL must not be empty.", nameof(url));
            if (url.Length > 2048) throw new ArgumentException("Image URL too long.", nameof(url));
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute)) throw new ArgumentException("Image URL is not a valid absolute URL.", nameof(url));

            Url = url;
        }

        public static implicit operator string(ImageUrl u) => u?.Url;
        public static explicit operator ImageUrl(string s) => s is null ? null : new ImageUrl(s);

        public override string ToString() => Url;
    }
}
