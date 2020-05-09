using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Persistance
{
    public class FixitDbContext : IdentityDbContext<User, UserRole, int>, IFixitDbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<RepairService> RepairServices { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderImage> OrderImages { get; set; }
        public DbSet<OrderOffer> OrderOffers { get; set; }

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
            modelBuilder.HasSequence("UserSequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("CategorySequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("SubCategorySequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("OpinionSequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("LocationSequence")
                .StartsAt(1)
                .IncrementsBy(10);

            modelBuilder.HasSequence("ImagesSequence")
                .StartsAt(1)
                .IncrementsBy(10);


            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FixitDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}