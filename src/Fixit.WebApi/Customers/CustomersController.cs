using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Services;
using Fixit.WebApi.Controllers;
using Fixit.Application.Customers.Commands.RegisterCustomer;
using Fixit.Application.Customers.Commands.UpdatePersonalData;
using Fixit.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Customers
{
  public class CustomersController : BaseController
  {
    //private const string AddCustomerAsyncRouteName = "AddCustomerAsync";
    [HttpPost]
    [ProducesResponseType(200)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterCustomerAsync([FromBody] RegisterCustomerCommand command)
    {
      return await HandleCommandAsync(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [Authorize(Policy = RolePolicies.RequireCustomer)]
    public async Task<IActionResult> UpdateContractorPersonalDataAsync([FromRoute] int id,
        [FromBody] UpdatePersonalDataCommand command)
    {
        if (!CurrentUserService.IsUser(id))
        {
            return BadRequest();
        }

        command.Id = id;

        return await HandleCommandAsync(command);
    }

        public CustomersController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
    }
  }
}
