using FluentValidation;

namespace Fixit.Application.Orders.Commands.CreateDirectOrder
{
    public class CreateDirectOrderCommandValidator : AbstractValidator<CreateDirectOrderCommand>
    {
        public CreateDirectOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.SubcategoryId)
                .NotNull()
                .NotEmpty();


            RuleFor(x => x.PlaceId)
                .NotNull()
                .NotEmpty();
            RuleFor(x => x.Latitude)
                .NotNull();
            RuleFor(x => x.Longitude)
                .NotNull();
    }
  }
}