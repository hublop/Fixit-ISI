using FluentValidation;

namespace Fixit.Application.Categories.Commands.AddSubcategory
{
    public class AddSubcategoryCommandValidator : AbstractValidator<AddSubcategoryCommand>
    {
        public AddSubcategoryCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(4000);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

        }
    }
}