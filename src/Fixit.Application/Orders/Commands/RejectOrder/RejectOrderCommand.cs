
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.RejectOrder
{
    public class RejectOrderCommand : ICommand
    {
        public int OrderId { get; set; }
        public int ContractorId { get; set; }
    }
}