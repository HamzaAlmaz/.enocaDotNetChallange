using Core.Application.DTOs.carrierConfiguration;
using Core.Application.Wrappers;
using Core.Domain.Entities;

namespace Core.Application.Interfaces.Services
{
    public interface ICarrierConfigurationService
    {
        Task<ServiceResponse<string>> CreateAsync(CreateCarrierConfigurationDTO dto);
        Task<ServiceResponse<CarrierConfiguration>> GetByIdAsync(int id);
        Task<ServiceResponse<List<CarrierConfiguration>>> GetAllAsync();
        Task<ServiceResponse<string>> UpdateAsync(int id, UpdateCarrierConfigurationDTO dto);
        Task<ServiceResponse<string>> DeleteAsync(int id);
    }
}