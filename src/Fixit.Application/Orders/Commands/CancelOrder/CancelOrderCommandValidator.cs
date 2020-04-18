using FluentValidation;

namespace Fixit.Application.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(x => x.OrderId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.CustomerId)
                .NotNull()
                .NotEmpty();
        }
    }
}