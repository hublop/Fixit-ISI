using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Orders.Events.DirectOrderCreated;
using Fixit.Application.Orders.Events.DistributedOrderSent;
using Fixit.Domain.Entities;
using Fixit.EventBus;
using Fixit.EventBus.Abstractions;
using Fixit.Shared.CQRS;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SubscriptionStatus = Fixit.Application.Contractors.Constants.SubscriptionStatus;

namespace Fixit.Application.Orders.Commands.NotifyAboutOrder
{
    public class NotifyAboutOrdersCommandHandler : ICommandHandler<NotifyAboutOrdersCommand>
    {
        public const string ClosestContractorsConfigSection = "ClosestContractorsDistance";
        private readonly ILogger<NotifyAboutOrdersCommandHandler> _logger;
        private readonly IFixitDbContext _dbContext;
        private readonly IEventBus _eventBus;
        private readonly IConfiguration _configuration;

    public NotifyAboutOrdersCommandHandler(ILogger<NotifyAboutOrdersCommandHandler> logger, IFixitDbContext dbContext, IEventBus eventBus, IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _eventBus = eventBus;
            _configuration = configuration;
        }

        //todo: add sms and mail notification sending
        public async Task<Unit> Handle(NotifyAboutOrdersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogWarning(
                $"Notification about orders in command handler. TimeAfterNonPremiumGetNotificationInMinutes: {request.TimeAfterNonPremiumGetNotificationInMinutes}");

            var distributedOrdersQuery = _dbContext.Orders
                .Include(x => x.OrderOffers)
                .Include(x => x.Location)
                .Where(x => x.IsDistributed && x.OrderOffers.All(y => y.OrderId != x.Id)).AsQueryable();

            var premiumMembers = await _dbContext.Contractors
                .Include(x => x.Location)
                .Include(x => x.RepairServices)
                .Include(x => x.SubscriptionStatus)
                .Where(x => x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Active) ||
                            x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Cancelled))
                .ToListAsync(cancellationToken: cancellationToken);
            var notPremiumMembers = await _dbContext.Contractors
                .Include(x => x.Location)
                .Include(x => x.RepairServices)
                .Include(x => x.SubscriptionStatus)
                .Where(x => x.SubscriptionStatus == null || x.SubscriptionStatus.Status.Equals(SubscriptionStatus.Deactivated))
                .ToListAsync(cancellationToken: cancellationToken);

             var ordersToBeSentToPremium = await distributedOrdersQuery.Where(x => x.LastNotificationDate == null).ToListAsync(cancellationToken: cancellationToken);

             var distanceConfig = _configuration.GetSection(ClosestContractorsConfigSection);
             var distance = int.Parse(distanceConfig.Value);
            foreach (var order in ordersToBeSentToPremium)
            {
                var orderCoordinates = new GeoCoordinate(order.Location.Latitude ?? 0, order.Location.Longitude ?? 0);
                var contractorsWithRepairService = premiumMembers.Where(x =>
                    x.RepairServices.Select(x => x.SubCategoryId).Contains(order.SubcategoryId));
                var closestContractors = contractorsWithRepairService.Where(x => CalculateDistance(orderCoordinates, x.Location?.Latitude ?? 0, x.Location?.Longitude ?? 0) < distance);
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

            var query = await distributedOrdersQuery.Where(x => x.LastNotificationDate != null && x.IsSentToAll == false).ToListAsync(cancellationToken: cancellationToken);
            var ordersToBeSentToNormal = query.Where(x =>
                ((TimeSpan) (DateTime.Now - x.LastNotificationDate))
                .TotalMinutes >=
                request.TimeAfterNonPremiumGetNotificationInMinutes);


          foreach (var order in ordersToBeSentToNormal)
          {
              var orderCoordinates = new GeoCoordinate(order.Location.Latitude ?? 0, order.Location.Longitude ?? 0);
              var contractorsWithRepairService = notPremiumMembers.Where(x =>
                  x.RepairServices.Select(x => x.SubCategoryId).Contains(order.SubcategoryId));
        var closestContractors = contractorsWithRepairService.Where(x => CalculateDistance(orderCoordinates, x.Location?.Latitude ?? 0, x.Location?.Longitude ?? 0) < distance);
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

      try
      {
        await _dbContext.SaveChangesAsync(cancellationToken);
      }catch(Exception e)
      {
        throw e;
      }
              

          


          return Unit.Value;
        }

        private double CalculateDistance(GeoCoordinate orderCoordinates, double contractorLatitude, double contractorLongitude)
        {
            var distance =  orderCoordinates.GetDistanceTo(new GeoCoordinate(contractorLatitude, contractorLongitude));

            return distance;
        }
    }
}