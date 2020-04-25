using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class OpinionEntityTypeConfiguration : IEntityTypeConfiguration<Opinion>
    {
        public void Configure(EntityTypeBuilder<Opinion> builder)
        {
            builder.ToTable("Opinion");

            builder.Property(u => u.Id)
                .HasColumnName("OpinionId")
                .UseHiLo("OpinionSequence");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ContractorId)
                .HasColumnName("ContractorId")
                .IsRequired();

            builder.Property(x => x.SubcategoryId)
                .HasColumnName("SubcategoryId")
                .IsRequired();

            builder.Property(x => x.Comment)
                .HasColumnName("Comment")
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .HasColumnName("CreatedOn")
                .IsRequired();

            builder.OwnsOne(x => x.Rating)
                .Property(x => x.Punctuality)
                .HasColumnName("Punctuality")
                .IsRequired();

            builder.OwnsOne(x => x.Rating)
                .Property(x => x.Quality)
                .HasColumnName("Quality")
                .IsRequired();

            builder.OwnsOne(x => x.Rating)
                .Property(x => x.Involvement)
                .HasColumnName("Involvement")
                .IsRequired();

            builder.HasOne(x => x.RepairService)
                .WithMany(x => x.Opinions)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}