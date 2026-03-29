namespace Core.Application.DTOs.carrier
{
    public class UpdateCarrierDTO
    {
        public string CarrierName { get; set; }
        public int CarrierPlusDesiCost { get; set; }
        public bool CarrierIsActive { get; set; }
    }
}