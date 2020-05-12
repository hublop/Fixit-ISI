using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.EventBus.Abstractions;

namespace Fixit.Application.Subscriptions.Events.CancelSubscription
{
  public class SubscriptionCancelledIntegrationEventHandler: IIntegrationEventHandler<SubscriptionCancelledIntegrationEvent>
  {
    private readonly IFixitDbContext _dbContext;

    public SubscriptionCancelledIntegrationEventHandler(IFixitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Task Handle(SubscriptionCancelledIntegrationEvent @event)
    {
      throw new NotImplementedException();
    }
  }
}
