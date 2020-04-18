using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Orders.Commands.AcceptOrder;
using Fixit.Application.Orders.Commands.CancelOrder;
using Fixit.Application.Orders.Commands.CreateDirectOrder;
using Fixit.Application.Orders.Commands.CreateDistributedOrder;
using Fixit.Application.Orders.Commands.RejectOrder;
using Fixit.WebApi.Controllers;
using Fixit.WebApi.Orders.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Orders
{
    public class OrdersController: BaseController
    {

        [HttpPost("{contractorId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDirectOrderAsync([FromRoute] int contractorId, [FromBody] CreateDirectOrderCommandDto command)
        {
            var createOrderCommand = Mapper.Map<CreateDirectOrderCommand>(command);
            createOrderCommand.ContractorId = contractorId;
            return await HandleCommandAsync(createOrderCommand);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CreateDistributedOrderAsync([FromBody] CreateDistributedOrderCommand command)
        {

            return await HandleCommandAsync(command);
        }

        [HttpDelete("{orderId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> CancelOrderAsync([FromRoute] int orderId, [FromBody] CancelOrderCommandDto command)
        {
            var cancelOrderCommand = Mapper.Map<CancelOrderCommand>(command);
            cancelOrderCommand.OrderId = orderId;
            return await HandleCommandAsync(cancelOrderCommand);
        }

        [HttpPut("{orderId}/accept")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AcceptOrderAsync([FromRoute] int orderId,
            [FromBody] AcceptOrderCommandDto command)
        {
            var acceptOrderCommand = Mapper.Map<AcceptOrderCommand>(command);
            acceptOrderCommand.OrderId = orderId;

            return await HandleCommandAsync(acceptOrderCommand);
        }

        [HttpPut("{orderId}/reject")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> RejectOrderAsync([FromRoute] int orderId,
            [FromBody] RejectOrderCommandDto command)
        {
            var rejectOrderCommand = Mapper.Map<RejectOrderCommand>(command);
            rejectOrderCommand.OrderId = orderId;

            return await HandleCommandAsync(rejectOrderCommand);
        }

        public OrdersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    }
}
