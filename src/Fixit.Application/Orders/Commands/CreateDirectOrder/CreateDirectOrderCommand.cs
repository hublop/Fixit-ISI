
using System.Collections.Generic;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.CreateDirectOrder
{
    public class CreateDirectOrderCommand : ICommand
    {
        public string Description { get; set; }
        public int ContractorId { get; set; }
        public int SubcategoryId { get; set; }
        public int CustomerId { get; set; }
        public string PlaceId { get; set; }
        public List<string> Base64Photos { get; set; }
    }
}