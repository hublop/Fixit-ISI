using FluentValidation;

namespace Fixit.Application.Orders.Commands.AcceptOrder
{
    public class AcceptOrderCommandValidator : AbstractValidator<AcceptOrderCommand>
    {
        public AcceptOrderCommandValidator()
        {
            RuleFor(x => x.ContractorId)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.OrderId)
                .NotNull()
                .NotEmpty();
        }
    }
}