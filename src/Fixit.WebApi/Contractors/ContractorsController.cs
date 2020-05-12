using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Services;
using Fixit.Application.Contractors.Commands.AddOpinion;
using Fixit.Application.Contractors.Commands.AddRepairService;
using Fixit.Application.Contractors.Commands.RegisterContractor;
using Fixit.Application.Contractors.Commands.RemoveRepairService;
using Fixit.Application.Contractors.Queries.GetList;
using Fixit.Application.Contractors.Queries.GetProfile;
using Fixit.Shared.Pagination;
using Fixit.WebApi.Common;
using Fixit.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Contractors
{
    public class ContractorsController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterContractoryAsync([FromBody] RegisterContractorCommand command)
        {
            return await HandleCommandAsync(command);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ContractorForList>), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] PagingParams pagingParams,
            [FromQuery] ContractorsListFilter filter)
        {
            var query = new GetContractorsListQuery
            {
                ContractorsListFilter = filter,
                PagingParams = pagingParams
            };

            return await HandleQueryWithPagingAsync(query);
        }

        [HttpGet("{contractorId}")]
        [ProducesResponseType(typeof(ContractorProfile), 200)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfile([FromRoute] int contractorId)
        {
            return await HandleQueryAsync(new GetProfileQuery { ContractorId = contractorId });
        }

        [HttpPost("{contractorId}/services/{subcategoryId}")]
        [ProducesResponseType(200)]
        [Authorize(Policy = Roles.Contractor)]
        public async Task<IActionResult> AddRepairServiceAsync([FromRoute] int contractorId,
            [FromRoute] int subcategoryId)
        {
            if (!CurrentUserService.IsUser(contractorId))
            {
                return BadRequest();
            }

            var command = new AddRepairServiceCommand
            {
                ContractorId = contractorId,
                SubcategoryId = subcategoryId
            };

            return await HandleCommandAsync(command);
        }

        [HttpDelete("{contractorId}/services/{subcategoryId}")]
        [ProducesResponseType(200)]
        [Authorize(Policy = Roles.Contractor)]
        public async Task<IActionResult> RemoveRepairServiceAsync([FromRoute] int contractorId,
            [FromRoute] int subcategoryId)
        {
            if (!CurrentUserService.IsUser(contractorId))
            {
                return BadRequest();
            }

            var command = new RemoveRepairServiceCommand
            {
                ContractorId = contractorId,
                SubcategoryId = subcategoryId
            };

            return await HandleCommandAsync(command);
        }

        [HttpPost("{contractorId}/opinions")]
        [ProducesResponseType(200)]
        [AllowAnonymous]
        public async Task<IActionResult> AddOpinions([FromRoute] int contractorId, [FromBody] AddOpinionCommand command)
        {
            command.ContractorId = contractorId;

            return await HandleCommandAsync(command);
        }


        public ContractorsController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
        }
    }
}
