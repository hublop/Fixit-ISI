using FluentValidation;

namespace Fixit.Application.Contractors.Commands.UpdatePersonalData
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

            RuleFor(x => x.SelfDescription)
                .MaximumLength(400);

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.PlaceId)
                .NotNull()
                .NotEmpty();
        }
    }
}