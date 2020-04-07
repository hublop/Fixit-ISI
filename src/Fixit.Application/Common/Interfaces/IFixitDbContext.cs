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

        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}