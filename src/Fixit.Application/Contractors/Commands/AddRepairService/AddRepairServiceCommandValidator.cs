using FluentValidation;

namespace Fixit.Application.Contractors.Commands.AddRepairService
{
    public class AddRepairServiceCommandValidator : AbstractValidator<AddRepairServiceCommand>
    {
        public AddRepairServiceCommandValidator()
        {
            RuleFor(x => x.ContractorId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.SubcategoryId)
                .NotNull()
                .NotEmpty();
        }
    }
}