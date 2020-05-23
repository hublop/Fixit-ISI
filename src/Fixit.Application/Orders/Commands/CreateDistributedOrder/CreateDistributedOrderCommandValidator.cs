using FluentValidation;

namespace Fixit.Application.Orders.Commands.CreateDistributedOrder
{
    public class CreateDistributedOrderCommandValidator : AbstractValidator<CreateDistributedOrderCommand>
    {
        public CreateDistributedOrderCommandValidator()
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
        }
    }
}