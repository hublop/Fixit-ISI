using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("Location");

            builder.Property(x => x.Id)
                .HasColumnName("LocationId")
                .UseHiLo("LocationSequence")
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Contractors)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId);

            builder.HasMany(x => x.Orders)
                .WithOne(x => x.Location)
                .HasForeignKey(x => x.LocationId);
        }
    }
}