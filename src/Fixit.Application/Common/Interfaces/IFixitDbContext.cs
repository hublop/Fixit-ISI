using System.Threading;
using System.Threading.Tasks;
using Fixit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Common.Interfaces
{
    public interface IFixitDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Subcategory> Subcategories { get; set; }
        DbSet<Contractor> Contractors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<RepairService> RepairServices { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Opinion> Opinions { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderImage> OrderImages { get; set; }
        DbSet<OrderOffer> OrderOffers { get; set; }
        DbSet<SubscriptionStatus> SubscriptionStatuses { get; set; }

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}