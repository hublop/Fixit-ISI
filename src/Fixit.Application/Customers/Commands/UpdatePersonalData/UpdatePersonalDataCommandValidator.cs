using FluentValidation;

namespace Fixit.Application.Customers.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandValidator : AbstractValidator<UpdatePersonalDataCommand>
    {
        public UpdatePersonalDataCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty();
        }
    }
}