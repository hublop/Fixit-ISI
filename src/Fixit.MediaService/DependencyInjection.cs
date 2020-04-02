using Fixit.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.MediaService
{
    public static class MediaServiceProvider
    {
        public static IServiceCollection AddMediaService(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinaryMediaServiceOptions>(options =>
                config.GetSection(Constants.ConfigSections.Cloudinary).Bind(options));

            services.AddScoped<IImageService, CloudinaryMediaService>();

            return services;
        }
    }
}