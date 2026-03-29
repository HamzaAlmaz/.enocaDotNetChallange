using Core.Application.DTOs.carrier;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ICarrierService
    {
        Task<ServiceResponse<string>> AddAsync(CreateCarrierDTO dto);
        Task<ServiceResponse<Carrier>> GetByIdAsync(int id);
        Task<ServiceResponse<List<Carrier>>> GetAllAsync();
        Task<ServiceResponse<string>> UpdateAsync(int carrierId, UpdateCarrierDTO dto);
        Task<ServiceResponse<string>> DeleteAsync(int id);
    }
}