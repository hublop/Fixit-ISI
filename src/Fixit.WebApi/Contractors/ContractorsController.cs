using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Services;
using Fixit.Application.Contractors.Commands.RegisterContractor;
using Fixit.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Contractors
{
  public class ContractorsController: BaseController
  {
    [HttpPost]
    [ProducesResponseType(200)]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterContractoryAsync([FromBody] RegisterContractorCommand command)
    {
      return await HandleCommandAsync(command);
    }

    public ContractorsController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
    }
  }
}
