using FluentValidation;

namespace Core.Application.DTOs.carrier
{
    public class CreateCarrierValidator : AbstractValidator<CreateCarrierDTO>
    {
        public CreateCarrierValidator()
        {
            RuleFor(x => x.CarrierName)
                .NotEmpty().WithMessage("Kargo firması adı boş geçilemez.")
                .MaximumLength(100).WithMessage("Kargo firması adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.CarrierPlusDesiCost)
                .GreaterThanOrEqualTo(0).WithMessage("Artı desi maliyeti 0 veya daha büyük bir değer olmalıdır.");
        }
    }
}