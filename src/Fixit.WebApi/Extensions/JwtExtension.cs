using System;
using System.Text;
using Fixit.Infrastructure.Services.Identity;
using Fixit.WebApi.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Fixit.WebApi.Extensions
{
    internal static class JwtExtension
    {
        internal static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[ConfigKey.JwtKeySource]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            services.Configure<JwtOptions>(options =>
            {
                options.RefreshTokenExpiryTimeInMinutes = Convert.ToDouble(configuration[ConfigKey.RefreshTokenExpiryTimeInMinutesSource]);
                options.Audience = configuration[ConfigKey.JwtAudienceSource];
                options.Issuer = configuration[ConfigKey.JwtIssuerSource];
                options.SigningCredentials = signingCredentials;
                options.ValidFor =
                    TimeSpan.FromMinutes(Convert.ToDouble(configuration[ConfigKey.JwtTokenExpiryTimeInMinutesSource]));
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingCredentials.Key,
                ValidateIssuer = true,
                ValidIssuer = configuration[ConfigKey.JwtIssuerSource],
                ValidateAudience = true,
                ValidAudience = configuration[ConfigKey.JwtAudienceSource],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(o => { o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata =
                        Convert.ToBoolean(configuration[ConfigKey.RequireHttpsMetadataSource]);
                    options.IncludeErrorDetails = Convert.ToBoolean(configuration[ConfigKey.IncludeErrorDetailsSource]);
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            services.AddAuthorizationWithRoles();
        }
    }
}