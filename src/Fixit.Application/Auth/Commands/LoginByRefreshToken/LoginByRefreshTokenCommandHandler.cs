using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Services.Identity;
using Fixit.Shared.CQRS;

namespace Fixit.Application.Auth.Commands.LoginByRefreshToken
{
    public class LoginByRefreshTokenCommandHandler : ICommandHandler<LoginByRefreshTokenCommand, JsonWebToken>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IRefreshTokenProvider _refreshTokenProvider;

        public LoginByRefreshTokenCommandHandler(IJwtProvider jwtProvider, IRefreshTokenProvider refreshTokenProvider)
        {
            _jwtProvider = jwtProvider;
            _refreshTokenProvider = refreshTokenProvider;
        }

        public async Task<JsonWebToken> Handle(LoginByRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var isTokeValid = await _refreshTokenProvider.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);

            if (!isTokeValid)
            {
                throw new UnauthorizedException("Unauthorized", $"User with id: {request.UserId} has been unauthorized");
            }

            var token = await _jwtProvider.GenerateAccessAndRefreshTokenAsync(request.UserId, request.RefreshToken);

            if (token == null)
            {
                throw new UnauthorizedException("Unauthorized", $"User with id: {request.UserId} has been unauthorized");
            }

            return token;
        }
    }
}