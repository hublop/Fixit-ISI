using System.Threading.Tasks;
using AutoMapper;
using Fixit.Application.Auth.Commands.ChangePassword;
using Fixit.Application.Auth.Commands.LoginByCredentials;
using Fixit.Application.Auth.Commands.LoginByRefreshToken;
using Fixit.Application.Common.Services;
using Fixit.Application.Common.Services.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fixit.WebApi.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator, IMapper mapper, ICurrentUserService currentUserService) : base(mediator, mapper, currentUserService)
        {
        }

        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(typeof(JsonWebToken), 200)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginByCredentialsCommand loginByCredentials)
        {
            return await HandleCommandAsync(loginByCredentials);
        }

        [AllowAnonymous]
        [HttpPost("refresh_token")]
        [ProducesResponseType(typeof(JsonWebToken), 200)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] LoginByRefreshTokenCommand loginByRefreshToken)
        {
            return await HandleCommandAsync(loginByRefreshToken);
        }

        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangePasswordAsync([FromRoute] int userId,
            [FromBody] ChangePasswordCommand changePasswordCommand)
        {
            if (!CurrentUserService.IsUser(userId))
            {
                return BadRequest();
            }

            changePasswordCommand.UserId = userId;

            return await HandleCommandAsync(changePasswordCommand);
        }
    }
}