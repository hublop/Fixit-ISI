using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Persistance
{
    public class FixitDbContext : DbContext, IFixitDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken) > 0;
        }

        public FixitDbContext(DbContextOptions<FixitDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence("CategorySequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("SubCategorySequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FixitDbContext).Assembly);
        }
    }
}