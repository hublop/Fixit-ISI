using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class OrderOfferEntityTypeConfiguration : IEntityTypeConfiguration<OrderOffer>

    {
        public void Configure(EntityTypeBuilder<OrderOffer> builder)
        {
            builder.ToTable("OrderOffer");

            builder.HasKey(e => new { e.ContractorId, e.OrderId });

            builder.HasOne(x => x.Contractor)
                .WithMany(x => x.OrderOffers)
                .HasForeignKey(x => x.ContractorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Order)
                .WithMany(x => x.OrderOffers)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}