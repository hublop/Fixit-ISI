using System.Collections.Generic;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.CreateDistributedOrder
{
    public class CreateDistributedOrderCommand : ICommand
    {
        public string Description { get; set; }
        public int SubcategoryId { get; set; }
        public int CustomerId { get; set; }
        public string PlaceId { get; set; }
        public List<string> Base64Photos { get; set; }
    }
}
