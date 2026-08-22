using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modulith.Commerce.AdminUsers.Infrastructure.Configurations
{
    public class AdminUserConfig : IEntityTypeConfiguration<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser>
    {
        public void Configure(EntityTypeBuilder<Modulith.Commerce.AdminUser.Domain.AdminUsers.AdminUser> builder)
        {
            builder.ToTable("AdminUsers");
            builder.HasKey(x => x.Id);

            builder.HasQueryFilter(x => x.DeletedDate == null && x.DeletedById == null);

            builder.Property(x => x.KeyCloakId).IsRequired(false);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value).HasColumnName("Email").IsRequired().HasMaxLength(300);
            });

            builder.OwnsOne(x => x.FirstName, fn =>
            {
                fn.Property(n => n.Value).HasColumnName("FirstName").IsRequired().HasMaxLength(100);
            });

            builder.OwnsOne(x => x.LastName, ln =>
            {
                ln.Property(n => n.Value).HasColumnName("LastName").IsRequired().HasMaxLength(100);
            });

            builder.OwnsOne(x => x.Title, title =>
            {
                title.Property(t => t.Value).HasColumnName("Title").IsRequired().HasMaxLength(200);
            });

            builder.OwnsOne(x => x.PhoneNumber, phone =>
            {
                phone.OwnsOne(p => p.CountryCode, cc =>
                {
                    cc.Property(c => c.Code).HasColumnName("PhoneCountryCode").HasMaxLength(10);
                });
                phone.Property(p => p.Number).HasColumnName("PhoneNumber").HasMaxLength(20);
            });

            builder.OwnsOne(x => x.AvatarUrl, avatar =>
            {
                avatar.Property(a => a.Url).HasColumnName("AvatarUrl").HasMaxLength(1000);
            });

            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.ContractStartDate).IsRequired();
            builder.Property(x => x.ContractEndDate).IsRequired(false);
            builder.Property(x => x.OffboardedAt).IsRequired(false);
            builder.Property(x => x.MfaEnabled).IsRequired();

            builder.HasOne(x => x.Team)
                .WithMany()
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            builder.HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
