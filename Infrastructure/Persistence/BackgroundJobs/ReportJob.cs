using Core.Application.Interfaces.BackgroundJobs;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.BackgroundJobs
{
    public class ReportJob : IReportJob
    {
        private readonly AppDbContext _context;

        public ReportJob(AppDbContext context)
        {
            _context = context;
        }

        public async Task ExecuteReportAsync()
        {
            var reportData = _context.Orders
                .GroupBy(o => new { o.CarrierId, OrderDay = o.OrderDate.Date })
                .Select(g => new {
                    CarrierId = g.Key.CarrierId,
                    Date = g.Key.OrderDay,
                    TotalCost = g.Sum(o => o.OrderCarrierCost)
                }).ToList();

            foreach (var data in reportData)
            {
                var existing = _context.CarrierReports
                    .FirstOrDefault(r => r.CarrierId == data.CarrierId && r.CarrierReportDate == data.Date);

                if (existing != null)
                {
                    existing.CarrierCost = data.TotalCost;
                }
                else
                {
                    _context.CarrierReports.Add(new CarrierReport
                    {
                        CarrierId = data.CarrierId,
                        CarrierReportDate = data.Date,
                        CarrierCost = data.TotalCost
                    });
                }
            }
            await _context.SaveChangesAsync();
        }
    }
}