using System.Collections.Generic;
using Fixit.EventBus.Events;

namespace Fixit.Application.Orders.Events.OrderWithPhotosAdded
{
    public class OrderWithPhotosAddedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public List<string> Base64Photos { get; set; }
    }
}