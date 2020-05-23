using Fixit.EventBus.Events;

namespace Fixit.Application.Orders.Events.DirectOrderCreated
{
    public class DirectOrderCreatedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
    }
}