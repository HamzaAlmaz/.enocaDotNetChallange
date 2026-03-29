using System.Threading.Tasks;

namespace Core.Application.Interfaces.BackgroundJobs
{
    public interface IReportJob
    {
        Task ExecuteReportAsync();
    }
}