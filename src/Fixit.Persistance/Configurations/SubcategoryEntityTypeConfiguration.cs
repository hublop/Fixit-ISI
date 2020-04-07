using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fixit.Persistance.Configurations
{
    public class SubcategoryEntityTypeConfiguration : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder.ToTable("SubCategory");

            builder.Property(u => u.Id)
                .HasColumnName("SubCategoryId")
                .UseHiLo("SubCategorySequence");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .IsRequired();

            builder.Property(x => x.CreatedDate)
                .HasColumnName("CreatedDate")
                .IsRequired();

            builder.Property(x => x.ModifiedDate)
                .HasColumnName("ModifiedDate");

            builder.HasOne(x => x.Category)
                .WithMany(x => x.SubCategories)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}