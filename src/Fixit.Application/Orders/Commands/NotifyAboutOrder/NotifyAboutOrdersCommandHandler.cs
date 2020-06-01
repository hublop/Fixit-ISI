using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Orders.Events.DirectOrderCreated;
using Fixit.Application.Orders.Events.DistributedOrderSent;
using Fixit.Domain.Entities;
using Fixit.EventBus.Abstractions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SubscriptionStatus = Fixit.Application.Contractors.Constants.SubscriptionStatus;

namespace Fixit.Application.Orders.Commands.NotifyAboutOrder
{
    public class NotifyAboutOrdersCommandHandler : ICommandHandler<NotifyAboutOrdersCommand>
    {
        private readonly ILogger<NotifyAboutOrdersCommandHandler> _logger;
        private readonly IFixitDbContext _dbContext;
        private readonly IEventBus _eventBus;

    public NotifyAboutOrdersCommandHandler(ILogger<NotifyAboutOrdersCommandHandler> logger, IFixitDbContext dbContext, IEventBus eventBus)
        {
            _logger = logger;
            _dbContext = dbContext;
            _eventBus = eventBus;
        }

        //todo: add sms and mail notification sending
        public async Task<Unit> Handle(NotifyAboutOrdersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning(
                $"Notification about orders in command handler. TimeAfterNonPremiumGetNotificationInMinutes: {request.TimeAfterNonPremiumGetNotificationInMinutes}");

            var distributedOrdersQuery = _dbContext.Orders
                .Include(x => x.OrderOffers)
                .Where(x => x.IsDistributed && x.OrderOffers.All(y => y.OrderId != x.Id)).AsQueryable();

            var premiumMembers = await _dbContext.Contractors
                .Where(x => x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Active) ||
                            x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Cancelled))
                .ToListAsync(cancellationToken: cancellationToken);
            var notPremiumMembers = await _dbContext.Contractors
                .Where(x => x.SubscriptionStatus == null || x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Cancelled))
                .ToListAsync(cancellationToken: cancellationToken);

             var ordersToBeSentToPremium = await distributedOrdersQuery.Where(x => x.LastNotificationDate == null).ToListAsync(cancellationToken: cancellationToken);

            foreach (var order in ordersToBeSentToPremium)
            {
                // find closest
                var closestContractors = premiumMembers;
                foreach (var contractor in closestContractors)
                {
                    await _dbContext.DistributedOrderContractor.AddAsync(
                        new DistributedOrderContractor
                        {
                          Contractor = contractor,
                          Order = order
                        },
                            cancellationToken);
                    _eventBus.Publish(new DistributedOrderSentToContractorEvent { OrderId = order.Id, ContractorId = contractor.Id });
                }

                order.LastNotificationDate = DateTime.Now;
            }

            var query = await distributedOrdersQuery.Where(x => x.LastNotificationDate != null && x.IsSentToAll).ToListAsync(cancellationToken: cancellationToken);
            var ordersToBeSentToNormal = query.Where(x =>
                ((TimeSpan) (DateTime.Now - x.LastNotificationDate))
                .TotalMinutes >=
                request.TimeAfterNonPremiumGetNotificationInMinutes);


          foreach (var order in ordersToBeSentToNormal)
          {
            // find closest
            var closestContractors = notPremiumMembers;
            foreach (var contractor in closestContractors)
            {
                await _dbContext.DistributedOrderContractor.AddAsync(
                new DistributedOrderContractor
                {
                    Contractor = contractor,
                    ContractorId = contractor.Id,
                    Order = order,
                    OrderId = order.Id
                },
                cancellationToken);
                _eventBus.Publish(new DistributedOrderSentToContractorEvent{ OrderId = order.Id, ContractorId = contractor.Id});
        }

            order.LastNotificationDate = DateTime.Now;
            order.IsSentToAll = true;
          }


          await _dbContext.SaveChangesAsync(cancellationToken);


          return Unit.Value;
        }
    }
}