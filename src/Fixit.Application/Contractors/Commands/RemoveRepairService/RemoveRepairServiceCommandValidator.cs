using FluentValidation;

namespace Fixit.Application.Contractors.Commands.RemoveRepairService
{
    public class RemoveRepairServiceCommandValidator : AbstractValidator<RemoveRepairServiceCommand>
    {
        public RemoveRepairServiceCommandValidator()
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