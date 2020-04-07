using System.Threading.Tasks;
using Fixit.Shared.CQRS;
using Fixit.Shared.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Fixit.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

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

        protected async Task<IActionResult> HandleQueryAsync<TReturn>(IQuery<TReturn> query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}