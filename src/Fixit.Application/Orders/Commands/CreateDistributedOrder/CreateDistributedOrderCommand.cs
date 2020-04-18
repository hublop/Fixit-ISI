using System;
using System.Collections.Generic;
using System.Text;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.CreateDistributedOrder
{
    public class CreateDistributedOrderCommand : ICommand
    {
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public int CustomerId { get; set; }
        public int PlaceId { get; set; }
    }
}
