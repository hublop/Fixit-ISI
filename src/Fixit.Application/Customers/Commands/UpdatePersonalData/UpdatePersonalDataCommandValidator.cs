using FluentValidation;

namespace Fixit.Application.Customers.Commands.UpdatePersonalData
{
    public class UpdatePersonalDataCommandValidator : AbstractValidator<UpdateCustomerPersonalDataCommand>
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