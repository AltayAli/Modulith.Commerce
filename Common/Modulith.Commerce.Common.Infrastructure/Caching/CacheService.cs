using Microsoft.Extensions.Caching.Distributed;
using Modulith.Commerce.Common.Application.Caching;
using System.Buffers;
using System.Text.Json;

namespace Modulith.Commerce.Common.Infrastructure.Caching
{
    public class CacheService(IDistributedCache cache) : ICacheService
    {
        public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            byte[]? bytes = await cache.GetAsync(key, cancellationToken);
            return bytes is null ? default : Deserialize<T>(bytes);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
        {
            byte[]? bytes = Serialize(value);
            await cache.SetAsync(key, bytes, CacheOptions.Create(expiration), cancellationToken);
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(key, cancellationToken);
        }

        private T? Deserialize<T>(byte[]? bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes);
        }

        private byte[] Serialize<T>(T? value)
        {
            var arrayBuffer = new ArrayBufferWriter<byte>();

            using var jsonWriter = new Utf8JsonWriter(arrayBuffer);
            JsonSerializer.Serialize(jsonWriter, value);

            return arrayBuffer.WrittenSpan.ToArray();
        }
    }
}
