using eShop.Ordering.Application.Features.Order.Commands.UpdateOrder;

using FluentValidation;

namespace eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(data => data.UserName)
                .NotEmpty().WithMessage($"'{nameof(CheckoutOrderCommand.UserName)}' is required.")
                .MaximumLength(50).WithMessage($"'{nameof(CheckoutOrderCommand.UserName)}' must not exceed 50 characters.");

            RuleFor(data => data.EmailAddress)
                .NotEmpty().WithMessage($"'{nameof(CheckoutOrderCommand.EmailAddress)}' is required.");

            RuleFor(data => data.TotalPrice)
                .NotEmpty().WithMessage($"'{nameof(CheckoutOrderCommand.TotalPrice)}' is required.")
                .GreaterThan(0).WithMessage($"'{nameof(CheckoutOrderCommand.TotalPrice)}' must not exceed 50 characters.");
        }
    }
}
