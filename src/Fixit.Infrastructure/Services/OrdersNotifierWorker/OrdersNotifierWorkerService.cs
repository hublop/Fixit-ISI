using System;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Orders.Commands.NotifyAboutOrder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fixit.Infrastructure.Services.OrdersNotifierWorker
{
    public class OrdersNotifierWorkerService : BackgroundService
    {
        private readonly ILogger<OrdersNotifierWorkerService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly OrdersNotifierWorkerServiceOptions _options;

        public OrdersNotifierWorkerService(ILogger<OrdersNotifierWorkerService> logger,
            IServiceScopeFactory scopeFactory, IOptions<OrdersNotifierWorkerServiceOptions> optionsAccessor)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _options = optionsAccessor.Value ?? throw new ArgumentNullException(nameof(optionsAccessor.Value));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Service is starting...");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Beginning single job loop...");

                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(new NotifyAboutOrdersCommand(), stoppingToken);

                    _logger.LogDebug("Service job loop finished...");
                }
                catch (Exception e)
                {
                    _logger.LogCritical("Service job failed", e);
                }

                await Task.Delay(_options.ScanningIntervalInMiliseconds, stoppingToken);
            }

            _logger.LogInformation("Service is stopping...");
        }
    }
}