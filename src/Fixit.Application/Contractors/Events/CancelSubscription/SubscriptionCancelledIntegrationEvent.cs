using System;
using Fixit.EventBus.Events;

namespace Fixit.Application.Contractors.Events.CancelSubscription
{
  public class SubscriptionCancelledIntegrationEvent: IntegrationEvent
  {
    public string CustomerId { get; set; }

    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
