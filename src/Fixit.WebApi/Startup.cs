using System.Reflection;
using AutoMapper;
using Fixit.Application;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Common.Services;
using Fixit.Application.Contractors.Events.ActivateSubscription;
using Fixit.Application.Contractors.Events.CancelSubscription;
using Fixit.Application.Contractors.Events.DeactivateSubscription;
using Fixit.Application.Orders.Events.DirectOrderCreated;
using Fixit.Application.Orders.Events.OrderWithPhotosAdded;
using Fixit.Domain.Common;
using Fixit.EventBus.Abstractions;
using Fixit.EventBus.RabbitMQ;
using Fixit.Infrastructure;
using Fixit.Persistance;
using Fixit.Persistance.ExampleData;
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(ICurrentUserService)));

            services.RegisterEventBus(Configuration);

            services.AddMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<IFixitDbContext>(); })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddTransient<SubscriptionActivatedIntegrationEventHandler>();
            services.AddTransient<SubscriptionCancelledIntegrationEventHandler>();
            services.AddTransient<SubscriptionDeactivatedIntegrationEventHandler>();
            services.AddTransient<OrderWithPhotosAddedIntegrationEventHandler>();
            services.AddTransient<DirectOrderCreatedIntegrationEventHandler>();

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
      eventBus.Subscribe<OrderWithPhotosAddedIntegrationEvent, OrderWithPhotosAddedIntegrationEventHandler>();
      eventBus.Subscribe<DirectOrderCreatedIntegrationEvent, DirectOrderCreatedIntegrationEventHandler>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GlobalSeed seed)
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

            seed.Seed().GetAwaiter().GetResult();
    }
    }
}
