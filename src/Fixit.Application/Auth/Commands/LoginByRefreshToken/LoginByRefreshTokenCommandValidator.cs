using FluentValidation;

namespace Fixit.Application.Auth.Commands.LoginByRefreshToken
{
    public class LoginByRefreshTokenCommandValidator : AbstractValidator<LoginByRefreshTokenCommand>
    {
        public LoginByRefreshTokenCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty();
        }
    }
}