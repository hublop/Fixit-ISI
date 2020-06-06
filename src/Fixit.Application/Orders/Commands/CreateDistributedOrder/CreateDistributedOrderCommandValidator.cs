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

      //RuleFor(x => x.PlaceId)
      //    .NotNull()
      //    .NotEmpty();

      RuleFor(x => x.PlaceId)
          .Null()
          .When(x => x.Latitude != null && x.Longitude != null)
          .WithMessage("PlaceId cannot be specified together with Latitude and Longitude");

      RuleFor(x => x.Longitude)
          .Null()
          .When(x => x.PlaceId != null)
          .WithMessage("PlaceId cannot be specified together with Latitude and Longitude");

      RuleFor(x => x.Latitude)
          .Null()
          .When(x => x.PlaceId != null)
          .WithMessage("PlaceId cannot be specified together with Latitude and Longitude");

      RuleFor(x => x.Latitude)
          .NotNull()
          .When(x => x.Longitude != null)
          .WithMessage("Both Longitude and Latitude need to be specified");

      RuleFor(x => x.Longitude)
          .NotNull()
          .When(x => x.Latitude != null)
          .WithMessage("Both Longitude and Latitude need to be specified");
    }
    }
}