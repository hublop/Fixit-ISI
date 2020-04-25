using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class RepairServiceEntityTypeConfiguration : IEntityTypeConfiguration<RepairService>
    {
        public void Configure(EntityTypeBuilder<RepairService> builder)
        {
            builder.ToTable("RepairService");

            builder.HasKey(x => new { x.ContractorId, x.SubCategoryId });

            builder.Property(x => x.ContractorId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.SubCategoryId)
                .HasColumnName("SubcategoryId")
                .IsRequired();

            builder.Property(x => x.Price)
                .HasColumnName("Price")
                .IsRequired();

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.RepairServices)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}