using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modulith.Commerce.Products.Domain.StaffMembers;

namespace Modulith.Commerce.Products.Infrastructure.Configurations
{
    public class StaffMemberConfig : IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            builder.ToTable("StaffMembers");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Email).HasColumnName("Email").IsRequired().HasMaxLength(300);
            builder.Property(x => x.FullName).HasColumnName("FullName").IsRequired().HasMaxLength(200);
            builder.Property(x => x.Status).HasColumnName("Status").IsRequired().HasMaxLength(50);
            builder.Property(x => x.TeamId).IsRequired(false);
        }
    }
}
