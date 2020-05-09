using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Services.Identity;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Auth.Commands.LoginByCredentials
{
    public class LoginByCredentialsCommandHandler : ICommandHandler<LoginByCredentialsCommand, JsonWebToken>
    {
        private readonly IJwtProvider _jwtProvider;

        public LoginByCredentialsCommandHandler(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;
        }

        public async Task<JsonWebToken> Handle(LoginByCredentialsCommand request, CancellationToken cancellationToken)
        {
            var token = await _jwtProvider.GenerateAccessAndRefreshTokenAsync(request.Email, request.Password);

            if (token == null)
            {
                throw new UnauthorizedException("Unauthorized",
                    $"User with email: {request.Email} has been unauthorized");
            }

            return token;
        }
    }
}