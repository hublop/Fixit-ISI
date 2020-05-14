using Fixit.Application;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Events.ActivateSubscription;
using Fixit.Application.Contractors.Events.CancelSubscription;
using Fixit.Application.Contractors.Events.DeactivateSubscription;
using Fixit.Application.Test;
using Fixit.Domain.Common;
using Fixit.EventBus.Abstractions;
using Fixit.EventBus.RabbitMQ;
using Fixit.Infrastructure;
using Fixit.Persistance;
using Fixit.WebApi.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fixit.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddPersistence(Configuration);
            services.AddApplication(Configuration);

            services.RegisterEventBus(Configuration);

            services.AddMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<IFixitDbContext>(); })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddTransient<SubscriptionActivatedIntegrationEvent>();
            services.AddTransient<SubscriptionCancelledIntegrationEvent>();
            services.AddTransient<SubscriptionDeactivatedIntegrationEvent>();

            services.ConfigureJwt(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "App name",
                    Description = "Description"
                });
            });

        }

    private void ConfigureEventBus(IApplicationBuilder app)
    {
      var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
      eventBus.Subscribe<SubscriptionActivatedIntegrationEvent, SubscriptionActivatedIntegrationEventHandler>();
      eventBus.Subscribe<SubscriptionCancelledIntegrationEvent, SubscriptionCancelledIntegrationEventHandler>();
      eventBus.Subscribe<SubscriptionDeactivatedIntegrationEvent, SubscriptionDeactivatedIntegrationEventHandler>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            

            ConfigureEventBus(app);
        }
    }
}
