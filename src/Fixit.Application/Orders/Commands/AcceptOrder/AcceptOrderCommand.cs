
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderCommand : ICommand
    {
        public int OrderId { get; set; }
        public int ContractorId { get; set; }
    }
}