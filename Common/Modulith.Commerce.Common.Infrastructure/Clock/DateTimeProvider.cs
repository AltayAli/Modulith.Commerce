using Modulith.Commerce.Common.Application.Abstractions;

namespace Modulith.Commerce.Common.Infrastructure.Clock
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
