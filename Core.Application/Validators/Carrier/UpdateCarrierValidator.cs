using FluentValidation;

namespace Core.Application.DTOs.carrier
{
    public class UpdateCarrierValidator : AbstractValidator<UpdateCarrierDTO>
    {
        public UpdateCarrierValidator()
        {
            RuleFor(x => x.CarrierName)
                .NotEmpty().WithMessage("Kargo firması adı boş geçilemez.")
                .MaximumLength(100).WithMessage("Kargo firması adı en fazla 100 karakter olabilir.");

            RuleFor(x => x.CarrierPlusDesiCost)
                .GreaterThanOrEqualTo(0).WithMessage("Artı desi maliyeti 0 veya daha büyük bir değer olmalıdır.");

            RuleFor(x => x.CarrierIsActive)
                .NotNull().WithMessage("Aktiflik durumu boş geçilemez.");
        }
    }
}