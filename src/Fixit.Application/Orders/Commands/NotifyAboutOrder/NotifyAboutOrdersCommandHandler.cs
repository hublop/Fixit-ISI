using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fixit.Application.Orders.Commands.NotifyAboutOrder
{
    public class NotifyAboutOrdersCommandHandler : ICommandHandler<NotifyAboutOrdersCommand>
    {
        private readonly ILogger<NotifyAboutOrdersCommandHandler> _logger;
        private readonly IFixitDbContext _dbContext;

        public NotifyAboutOrdersCommandHandler(ILogger<NotifyAboutOrdersCommandHandler> logger, IFixitDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(NotifyAboutOrdersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning(
                $"Notification about orders in command handler. TimeAfterNonPremiumGetNotificationInMinutes: {request.TimeAfterNonPremiumGetNotificationInMinutes}");
            return Unit.Value;
        }
    }
}