using Core.Domain.Entities;

public class CarrierReport
{
    public int CarrierReportId { get; set; }
    public int CarrierId { get; set; }
    public decimal CarrierCost { get; set; }
    public DateTime CarrierReportDate { get; set; }

    public Carrier Carrier { get; set; }
}