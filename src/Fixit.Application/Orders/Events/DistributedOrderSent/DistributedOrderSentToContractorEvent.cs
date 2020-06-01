using System;
using System.Collections.Generic;
using System.Text;
using Fixit.EventBus.Events;

namespace Fixit.Application.Orders.Events.DistributedOrderSent
{
  public class DistributedOrderSentToContractorEvent: IntegrationEvent
  {
      public int OrderId { get; set; }
      public int ContractorId { get; set; }
  }
}
