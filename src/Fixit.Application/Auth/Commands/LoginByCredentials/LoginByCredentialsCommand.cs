using Fixit.Application.Common.Services.Identity;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Auth.Commands.LoginByCredentials
{
    public class LoginByCredentialsCommand : ICommand<JsonWebToken>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}