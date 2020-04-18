
using Fixit.Shared.CQRS;

namespace Fixit.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : ICommand
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
    }
}