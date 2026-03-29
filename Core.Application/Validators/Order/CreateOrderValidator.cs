using FluentValidation;

namespace Core.Application.DTOs.order
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.OrderDesi)
                .GreaterThan(0).WithMessage("Desi değeri 0'dan büyük olmalıdır.");
        }
    }
}