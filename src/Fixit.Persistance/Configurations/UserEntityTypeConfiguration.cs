using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.Property(u => u.Id)
                .HasColumnName("UserId")
                .UseHiLo("UserSequence");

            builder.HasDiscriminator<string>("UserType")
                .HasValue<Contractor>("Contractor")
                .HasValue<Customer>("Customer");

            builder.HasOne(x => x.Image)
                .WithMany(x => x.UserImages)
                .HasForeignKey(x => x.ImageId);
        }
    }
}