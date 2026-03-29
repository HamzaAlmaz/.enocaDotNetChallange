using FluentValidation;

namespace Core.Application.DTOs.carrierConfiguration
{
    public class CreateCarrierConfigurationValidator : AbstractValidator<CreateCarrierConfigurationDTO>
    {
        public CreateCarrierConfigurationValidator()
        {
            RuleFor(x => x.CarrierId)
                .GreaterThan(0).WithMessage("Kargo firması seçilmelidir.");

            RuleFor(x => x.CarrierMinDesi)
                .GreaterThanOrEqualTo(0).WithMessage("Minimum desi 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.CarrierMaxDesi)
                .GreaterThan(0).WithMessage("Maksimum desi 0'dan büyük olmalıdır.")
                .GreaterThan(x => x.CarrierMinDesi).WithMessage("Maksimum desi, minimum desiden büyük olmalıdır.");

            RuleFor(x => x.CarrierCost)
                .GreaterThanOrEqualTo(0).WithMessage("Kargo ücreti 0 veya daha büyük olmalıdır.");
        }
    }
}