using Fixit.Application.Common.Services;
using Fixit.Application.Common.Services.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.Infrastructure.Services.Mail
{
    public static class MailServiceProvider
    {
        public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<SendgridMailServiceOptions>(options =>
                config.GetSection(Constants.ConfigSections.SendGrid).Bind(options));

            services.AddScoped<IMailService, SendgridMailService>();

            return services;
        }
    }
}