using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class ContractorEntityTypeConfiguration : IEntityTypeConfiguration<Contractor>
    {
        public void Configure(EntityTypeBuilder<Contractor> builder)
        {
            builder.Property(x => x.CompanyName)
                .HasColumnName("CompanyName")
                .HasMaxLength(40);

            builder.Property(x => x.SelfDescription)
                .HasColumnName("SelfDescription")
                .HasMaxLength(4000);

            builder.Property(x => x.ContractorFrom)
                .HasColumnName("ContractorFrom");

            builder.HasMany(x => x.RepairServices)
                .WithOne(x => x.Contractor)
                .HasForeignKey(x => x.ContractorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Metadata
                .FindNavigation(nameof(Contractor.RepairServices))
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasOne(x => x.Location)
                .WithMany()
                .HasForeignKey(x => x.LocationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(x => x.SubscriptionStatus)
                .WithMany()
                .HasForeignKey(x => x.SubscriptionStatusId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}