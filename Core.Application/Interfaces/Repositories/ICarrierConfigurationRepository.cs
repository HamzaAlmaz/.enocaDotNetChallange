using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICarrierConfigurationRepository
    {
        Task<int> AddAsync(CarrierConfiguration carrierConfiguration);
        Task<CarrierConfiguration?> GetByIdAsync(int id);
        Task<List<CarrierConfiguration>> GetAllAsync();
        Task UpdateAsync(CarrierConfiguration carrierConfiguration);
        Task<bool> DeleteAsync(int id);
    }
}