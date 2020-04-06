using System;
using Fixit.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Fixit.EventBus.RabbitMQ
{
    public static class EventBusProvider
    {
        public static IServiceCollection RegisterEventBus(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EventBusOptions>(options =>
                config.GetSection(Constants.ConfigSections.RabbitMQEventBus).Bind(options));

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IRabbitMQPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var options = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;

                var connection = new ConnectionFactory()
                {
                    Uri = new Uri(options.Uri),
                    DispatchConsumersAsync = true
                };

                var retryCount = options.RetryCount > 0 ? options.RetryCount : 5;

                return new DefaultRabbitMQPersistentConnection(connection, logger, retryCount);
            });
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(provider =>
            {
                var connection = provider.GetRequiredService<IRabbitMQPersistentConnection>();
                var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
                var logger = provider.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = provider.GetRequiredService<IEventBusSubscriptionsManager>();
                var options = provider.GetRequiredService<IOptions<EventBusOptions>>().Value;
                var retryCount = options.RetryCount > 0 ? options.RetryCount : 5;

                return new EventBusRabbitMQ(connection, logger, scopeFactory, eventBusSubcriptionsManager,
                    options.SubscriptionClientName, retryCount);
            });

            return services;
        }
    }
}