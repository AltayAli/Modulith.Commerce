using System.Text.RegularExpressions;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.Products.Domain.Products
{
    public partial record Slug : ValueObject
    {
        private const int MaxLength = 280;

        private static readonly Dictionary<char, char> TurkishCharacterMap = new()
        {
            ['ğ'] = 'g',
            ['ş'] = 's',
            ['ı'] = 'i',
            ['ö'] = 'o',
            ['ü'] = 'u',
            ['ç'] = 'c',
            ['İ'] = 'i',
            ['Ğ'] = 'g',
            ['Ş'] = 's',
            ['Ö'] = 'o',
            ['Ü'] = 'u',
            ['Ç'] = 'c'
        };

        public string Value { get; }

        public Slug(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Slug cannot be null or empty.", nameof(value));

            if (value.Length > MaxLength)
                throw new ArgumentException($"Slug cannot exceed {MaxLength} characters.", nameof(value));

            if (!SlugRegex().IsMatch(value))
                throw new ArgumentException("Slug format is invalid. Expected lowercase alphanumeric segments separated by single hyphens.", nameof(value));

            Value = value;
        }

        public static implicit operator string(Slug s) => s?.Value;
        public static explicit operator Slug(string s) => new Slug(s);
        public override string ToString() => Value;

        public static Slug GenerateFrom(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));

            string folded = FoldTurkishCharacters(name.Trim());
            string lowered = folded.ToLowerInvariant();
            string slugified = NonAlphaNumericRegex().Replace(lowered, "-");
            slugified = MultipleDashesRegex().Replace(slugified, "-").Trim('-');

            if (slugified.Length > MaxLength)
                slugified = slugified[..MaxLength].Trim('-');

            return new Slug(slugified);
        }

        private static string FoldTurkishCharacters(string value)
        {
            var chars = new char[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                chars[i] = TurkishCharacterMap.TryGetValue(value[i], out char replacement)
                    ? replacement
                    : value[i];
            }

            return new string(chars);
        }

        [GeneratedRegex("^[a-z0-9]+(-[a-z0-9]+)*$")]
        private static partial Regex SlugRegex();

        [GeneratedRegex("[^a-z0-9]+")]
        private static partial Regex NonAlphaNumericRegex();

        [GeneratedRegex("-{2,}")]
        private static partial Regex MultipleDashesRegex();
    }
}
