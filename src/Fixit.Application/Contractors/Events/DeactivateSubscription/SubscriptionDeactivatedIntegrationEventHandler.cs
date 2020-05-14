using System;
using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.Application.Contractors.Constants;
using Fixit.Application.Contractors.Exceptions;
using Fixit.EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Fixit.Application.Contractors.Events.DeactivateSubscription
{
  public class SubscriptionDeactivatedIntegrationEventHandler: IIntegrationEventHandler<SubscriptionDeactivatedIntegrationEvent>
  {
    private readonly IFixitDbContext _dbContext;

    public SubscriptionDeactivatedIntegrationEventHandler(IFixitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task Handle(SubscriptionDeactivatedIntegrationEvent @event)
    {
      var contractor = await _dbContext.Contractors.SingleOrDefaultAsync(x => x.ContractorUUID.Equals(@event.CustomerId));

      if (contractor == null)
      {
        throw new ContractorDoesNotExistException(@event.CustomerId);
      }

      var subscriptionStatus =
        await _dbContext.SubscriptionStatuses.SingleAsync(x => x.Status.Equals(SubscriptionStatus.Deactivated));

      contractor.SubscriptionStatus = subscriptionStatus;

      await _dbContext.SaveChangesAsync(CancellationToken.None);
    }
  }
}
