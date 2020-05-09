using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Common.Services;
using Fixit.Shared.CQRS;
using Fixit.Shared.Pagination;
using Fixit.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Policy = RolePolicies.RequireAuthorizedUser)]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator;
        protected IMapper Mapper;
        protected ICurrentUserService CurrentUserService;


        public BaseController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService)
        {
            Mediator = mediator;
            Mapper = mapper;
            CurrentUserService = currentUserService;
        }

        protected async Task<IActionResult> HandleCommandWithLocationResultAsync(ICommand<int> command,
            string accessRouteName)
        {
            var id = await Mediator.Send(command);
            return CreatedAtRoute(accessRouteName, new { id = id}, command);
        }

        protected async Task<IActionResult> HandleQueryWithPagingAsync<TReturn>(IQuery<PaginatedResult<TReturn>> query)
        {
            var paginatedResult = await Mediator.Send(query);
            HttpContext.Response.AddPagination(paginatedResult.GetHeader());
            return Ok(paginatedResult.Result);
        }

        protected async Task<IActionResult> HandleCommandAsync(ICommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        protected async Task<IActionResult> HandleCommandAsync<T>(ICommand<T> command)
        {
            return Ok(await Mediator.Send(command));
        }

        protected async Task<IActionResult> HandleQueryAsync<TReturn>(IQuery<TReturn> query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}