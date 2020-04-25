using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasMany(x => x.OrderImages)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.HasMany(x => x.OrderOffers)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder.HasOne(x => x.Subcategory)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.SubcategoryId);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
        }
    }
}