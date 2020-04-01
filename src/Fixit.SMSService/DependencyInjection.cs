using Fixit.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.SmsService
{
    public static class SmsServiceProvider
    {
        public static IServiceCollection AddSmsService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<TwilioSmsServiceOptions>(options =>
                config.GetSection(Constants.ConfigSections.Twilio).Bind(options));

            services.AddScoped<ISmsService, TwilioSmsService>();

            return services;
        }
    }
}