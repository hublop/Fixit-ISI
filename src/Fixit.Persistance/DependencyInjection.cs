using Fixit.Application.Common.Interfaces;
using Fixit.Domain.Entities;
using Fixit.Persistance.ExampleData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.Persistance
{
    public static class PersistanceProvider
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FixitDbContext>(options =>
                {
                    options.UseSqlServer(configuration[Constants.ConfigKey.DbConntectionSource]);
                });

            services.AddIdentity<User, UserRole>()
                .AddEntityFrameworkStores<FixitDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IFixitDbContext>(provider => provider.GetService<FixitDbContext>());

            services.AddScoped(typeof(GlobalSeed));

      return services;
        }
    }
}