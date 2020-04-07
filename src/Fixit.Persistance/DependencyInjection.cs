using Fixit.Application.Common.Interfaces;
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

            services.AddScoped<IFixitDbContext>(provider => provider.GetService<FixitDbContext>());

            return services;
        }
    }
}