using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Domain.Abstractions;
using Modulith.Commerce.Products.Domain.Abstractions;
using Modulith.Commerce.Products.Infrastructure.Outbox;

namespace Modulith.Commerce.Products.Infrastructure.Data
{
    public class ProductsDbContext : DbContext, IUnitOfWork
    {
        private JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All
        };
        private readonly IDateTimeProvider _dateTimeProvider;
        public ProductsDbContext(IDateTimeProvider dateTimeProvider, DbContextOptions<ProductsDbContext> options) : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("products");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddDomainEventAsOutboxMessage();

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        private void AddDomainEventAsOutboxMessage()
        {
            var outboxMessages = ChangeTracker.Entries<BaseEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.GetDomainEvents().ToList();
                    entity.ClearDomainEvents();

                    return events;
                })
                .Select(domainEvent => new OutboxMessage(
                    JsonConvert.SerializeObject(domainEvent, _jsonSerializerSettings),
                    domainEvent.GetType().Name,
                    _dateTimeProvider.UtcNow))
                .ToList();

            AddRange(outboxMessages);
        }
    }
}
