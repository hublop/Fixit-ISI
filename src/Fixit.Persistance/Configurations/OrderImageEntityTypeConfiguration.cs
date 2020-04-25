using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class OrderImageEntityTypeConfiguration : IEntityTypeConfiguration<OrderImage>
    {
        public void Configure(EntityTypeBuilder<OrderImage> builder)
        {
            builder.ToTable("OrderImage");

            builder.HasKey(x => new {x.ImageId, x.OrderId});

            builder.HasOne(x => x.Image)
                .WithMany(x => x.OrderImages)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderImages)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}