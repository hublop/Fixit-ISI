using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Image");

            builder.Property(x => x.Id)
                .HasColumnName("ImageId")
                .UseHiLo("ImagesSequence")
                .IsRequired();

            builder.HasMany(x => x.OrderImages)
                .WithOne(x => x.Image)
                .HasForeignKey(x => x.ImageId);

            builder.HasMany(x => x.UserImages)
                .WithOne(x => x.Image)
                .HasForeignKey(x => x.ImageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}