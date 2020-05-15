using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.Infrastructure.Services.OrdersNotifierWorker
{
    public static class OrdersNotifierWorkerServiceProvider
    {
        public static IServiceCollection AddOrderNotifierWorker(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<OrdersNotifierWorkerServiceOptions>(options =>
                config.GetSection(Constants.ConfigSections.OrdersNotifier).Bind(options));
            
            services.AddHostedService<OrdersNotifierWorkerService>();

            return services;
        }
    }
}