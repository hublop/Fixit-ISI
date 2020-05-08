using System.Threading;
using System.Threading.Tasks;
using Fixit.Application.Common.Exceptions;
using Fixit.Application.Common.Services.Identity;
using Fixit.Shared.CQRS;
using MediatR;

namespace Fixit.Application.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
    {
        private readonly IPasswordManager _passwordManager;

        public ChangePasswordCommandHandler(IPasswordManager passwordManager)
        {
            _passwordManager = passwordManager;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!await _passwordManager.IsPasswordValid(request.UserId, request.OldPassword))
            {
                throw new UnauthorizedException("Unauthorized", $"User with id: {request.UserId} has been unauthorized");
            }

            if (!await _passwordManager.UpdatePassword(request.UserId, request.OldPassword, request.OldPassword))
            {
                throw new UnauthorizedException("Unauthorized", $"User with id: {request.UserId} has been unauthorized");
            }

            return Unit.Value;
        }
    }
}