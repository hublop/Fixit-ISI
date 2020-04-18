using FluentValidation;

namespace Fixit.Application.Orders.Commands.RejectOrder
{
    public class RejectOrderCommandValidator : AbstractValidator<RejectOrderCommand>
    {
        public RejectOrderCommandValidator()
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