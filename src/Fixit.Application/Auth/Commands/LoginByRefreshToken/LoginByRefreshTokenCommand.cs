using Fixit.Application.Common.Services.Identity;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Auth.Commands.LoginByRefreshToken
{
    public class LoginByRefreshTokenCommand : ICommand<JsonWebToken>
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
    }
}