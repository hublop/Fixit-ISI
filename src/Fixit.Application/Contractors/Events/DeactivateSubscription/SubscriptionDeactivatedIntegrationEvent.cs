using System;
using Fixit.EventBus.Events;

namespace Fixit.Application.Contractors.Events.DeactivateSubscription
{
  public class SubscriptionDeactivatedIntegrationEvent: IntegrationEvent
  {
    public string CustomerId { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
