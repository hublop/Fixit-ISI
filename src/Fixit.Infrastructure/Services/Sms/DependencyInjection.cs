using Fixit.Application.Common.Services;
using Fixit.Application.Common.Services.Sms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.Infrastructure.Services.Sms
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