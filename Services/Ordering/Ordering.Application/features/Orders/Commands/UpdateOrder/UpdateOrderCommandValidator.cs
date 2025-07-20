

using FluentValidation;

namespace Ordering.Application.features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandValidator:AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(a => a.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(a => a.EmailAddres)
                .NotEmpty().WithMessage("{Email} is required");

            RuleFor(a => a.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} should be greater than 0");
        }
    }
}
