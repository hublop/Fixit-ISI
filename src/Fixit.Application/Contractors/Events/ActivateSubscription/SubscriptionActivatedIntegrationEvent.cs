using System;
using Fixit.EventBus.Events;

namespace Fixit.Application.Contractors.Events.ActivateSubscription
{
  public class SubscriptionActivatedIntegrationEvent: IntegrationEvent
  {
    public string CustomerId { get; set; }
    public string SubscriptionId {get; set;}
    public string Status { get; set; }
    public DateTime NextPaymentDate { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}
