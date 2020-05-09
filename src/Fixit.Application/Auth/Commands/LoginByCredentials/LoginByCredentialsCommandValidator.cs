using FluentValidation;

namespace Fixit.Application.Auth.Commands.LoginByCredentials
{
    public class LoginByCredentialsCommandValidator : AbstractValidator<LoginByCredentialsCommand>
    {
        public LoginByCredentialsCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}