
using System.Threading.Tasks;
using AutoMapper;
using Fixit.WebApi.Controllers;
using Fixit.Application.Customers.Commands.RegisterCustomer;
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

    public CustomersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }
  }
}
