using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Services;
using Fixit.Application.Orders.Commands.AcceptOrder;
using Fixit.Application.Orders.Commands.CreateDirectOrder;
using Fixit.Application.Orders.Commands.CreateDistributedOrder;
using Fixit.Application.Orders.Queries.GetOffersForCustomer;
using Fixit.WebApi.Common;
using Fixit.WebApi.Controllers;
using Fixit.WebApi.Orders.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPut("{orderId}/accept")]
        [Authorize(Policy = RolePolicies.RequireContractor)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> AcceptOrderAsync([FromRoute] int orderId,
            [FromBody] AcceptOrderCommandDto command)
        {
            var acceptOrderCommand = Mapper.Map<AcceptOrderCommand>(command);
            acceptOrderCommand.OrderId = orderId;
            if (!CurrentUserService.IsUser(command.ContractorId))
            {
                return BadRequest();
            }

            return await HandleCommandAsync(acceptOrderCommand);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetOrderOffersForCustomerAsync([FromQuery] int customerId,
            [FromQuery] int? orderId)
        {
            return await HandleQueryAsync(new GetOffersForCustomerQuery {CustomerId = customerId, OrderId = orderId});
        }

        public OrdersController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
        }
    }
}
