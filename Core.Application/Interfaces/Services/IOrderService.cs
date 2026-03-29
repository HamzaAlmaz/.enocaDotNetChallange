using Core.Application.DTOs.order;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<string>> CreateAsync(CreateOrderDTO dto);
        Task<ServiceResponse<List<Order>>> GetAllAsync();
        Task<ServiceResponse<string>> DeleteAsync(int id);
    }
}