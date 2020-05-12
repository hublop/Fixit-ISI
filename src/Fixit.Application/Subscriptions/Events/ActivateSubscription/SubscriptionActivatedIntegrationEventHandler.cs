using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fixit.Application.Common.Interfaces;
using Fixit.EventBus.Abstractions;

namespace Fixit.Application.Subscriptions.Events.ActivateSubscription
{
  public class SubscriptionActivatedIntegrationEventHandler : IIntegrationEventHandler<SubscriptionActivatedIntegrationEvent>
  {
    private readonly IFixitDbContext _dbContext;

    public SubscriptionActivatedIntegrationEventHandler(IFixitDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public Task Handle(SubscriptionActivatedIntegrationEvent @event)
    {
      throw new NotImplementedException();
    }
  }
}
