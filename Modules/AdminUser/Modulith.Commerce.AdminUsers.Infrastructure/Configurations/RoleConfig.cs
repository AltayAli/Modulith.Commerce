using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.AdminUser.Domain.Roles;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.DeletedDate == null && x.DeletedById == null);

            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(n => n.Value).HasColumnName("Name").IsRequired().HasMaxLength(200);
            });

            builder.OwnsOne(x => x.Description, desc =>
            {
                desc.Property(d => d.Value).HasColumnName("Description").HasMaxLength(500);
            });

            builder.OwnsOne(x => x.KeycloakRoleName, kc =>
            {
                kc.Property(k => k.Value).HasColumnName("KeycloakRoleName").IsRequired().HasMaxLength(200);
            });

            builder.Property(x => x.IsSystemRole).IsRequired();

            builder.Property(x => x.Status).IsRequired();

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.RolePermissions)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
