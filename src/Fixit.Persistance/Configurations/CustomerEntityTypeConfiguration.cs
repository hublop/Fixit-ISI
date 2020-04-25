using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}