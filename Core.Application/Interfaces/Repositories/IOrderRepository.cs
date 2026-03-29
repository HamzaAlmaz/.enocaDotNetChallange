using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<int> AddAsync(Order order);
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<List<CarrierConfiguration>> GetCarrierConfigurationsAsync();
    }
}