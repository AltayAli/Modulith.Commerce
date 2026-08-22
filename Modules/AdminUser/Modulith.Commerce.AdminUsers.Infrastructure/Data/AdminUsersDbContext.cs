using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Modulith.Commerce.AdminUser.Domain.ActivityLogs;
using Modulith.Commerce.AdminUser.Domain.AdminUserRoles;
using Modulith.Commerce.AdminUser.Domain.Abstractions;
using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.AdminUser.Domain.Permissions;
using Modulith.Commerce.AdminUser.Domain.RolePermissions;
using Modulith.Commerce.AdminUser.Domain.Roles;
using Modulith.Commerce.AdminUser.Domain.Teams;
using Modulith.Commerce.AdminUsers.Infrastructure.Outbox;
using Modulith.Commerce.Common.Application.Abstractions;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Data
{
    public class AdminUsersDbContext : DbContext, IUnitOfWork
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPublisher _publisher;

        public AdminUsersDbContext(IDateTimeProvider dateTimeProvider, IPublisher publisher, DbContextOptions<AdminUsersDbContext> options)
            : base(options)
        {
            _dateTimeProvider = dateTimeProvider;
            _publisher = publisher;
        }

        public DbSet<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser> AdminUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<AdminUserRole> AdminUserRoles { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("adminusers");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdminUsersDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEvents = AddDomainEventAsOutboxMessage();
            int result = await base.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            return result;
        }

        private List<IDomainEvent> AddDomainEventAsOutboxMessage()
        {
            var domainEvents = ChangeTracker.Entries<BaseEntity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var events = entity.GetDomainEvents().ToList();
                    entity.ClearDomainEvents();
                    return events;
                })
                .ToList();

            var outboxMessages = domainEvents
                .Select(domainEvent => new OutboxMessage(
                    JsonConvert.SerializeObject(domainEvent, _jsonSerializerSettings),
                    domainEvent.GetType().Name,
                    _dateTimeProvider.UtcNow))
                .ToList();

            AddRange(outboxMessages);

            return domainEvents;
        }
    }
}
