using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Caching.Distributed;

namespace Modulith.Commerce.Common.Infrastructure.Auth
{
    public sealed class RedisTicketStore(IDistributedCache cache) : ITicketStore
    {
        private const string KeyPrefix = "session-";
        private static readonly TimeSpan DefaultExpiration = TimeSpan.FromHours(8);

        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            string key = KeyPrefix + Guid.NewGuid();

            await WriteTicketAsync(key, ticket);

            return key;
        }

        public async Task RenewAsync(string key, AuthenticationTicket ticket)
        {
            await WriteTicketAsync(key, ticket);
        }

        public async Task<AuthenticationTicket?> RetrieveAsync(string key)
        {
            byte[]? bytes = await cache.GetAsync(key);

            return bytes is null ? null : TicketSerializer.Default.Deserialize(bytes);
        }

        public async Task RemoveAsync(string key)
        {
            await cache.RemoveAsync(key);
        }

        private async Task WriteTicketAsync(string key, AuthenticationTicket ticket)
        {
            byte[] bytes = TicketSerializer.Default.Serialize(ticket);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = ticket.Properties.ExpiresUtc ?? DateTimeOffset.UtcNow.Add(DefaultExpiration)
            };

            await cache.SetAsync(key, bytes, options);
        }
    }
}
