using FluentValidation;

namespace Fixit.Application.Categories.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(4000);

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}