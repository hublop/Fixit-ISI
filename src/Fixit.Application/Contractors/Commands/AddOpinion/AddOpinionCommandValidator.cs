using FluentValidation;

namespace Fixit.Application.Contractors.Commands.AddOpinion
{
    public class AddOpinionCommandValidator : AbstractValidator<AddOpinionCommand>
    {
        public AddOpinionCommandValidator()
        {
            RuleFor(x => x.Punctuality)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Quality)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Involvement)
                .NotNull()
                .NotEmpty()
                .GreaterThan(0)
                .LessThanOrEqualTo(5);

            RuleFor(x => x.Comment)
                .NotNull()
                .NotEmpty()
                .MaximumLength(4000);

            RuleFor(x => x.ContractorId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.SubcategoryId)
                .NotNull()
                .NotEmpty();
        }
    }
}