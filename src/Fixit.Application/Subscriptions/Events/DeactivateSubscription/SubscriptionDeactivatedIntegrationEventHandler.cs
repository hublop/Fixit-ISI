using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.EventBus.Abstractions;

namespace Fixit.Application.Subscriptions.Events.DeactivateSubscription
{
  public class SubscriptionDeactivatedIntegrationEventHandler: IIntegrationEventHandler<SubscriptionDeactivatedIntegrationEvent>
  {
    private readonly IFixitDbContext _dbContext;

    public SubscriptionDeactivatedIntegrationEventHandler(IFixitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Task Handle(SubscriptionDeactivatedIntegrationEvent @event)
    {
      throw new NotImplementedException();
    }
  }
}
