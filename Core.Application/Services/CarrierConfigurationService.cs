using Core.Application.DTOs.carrierConfiguration;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CarrierConfigurationService : ICarrierConfigurationService
    {
        private readonly ICarrierConfigurationRepository _carrierConfigurationRepository;

        public CarrierConfigurationService(ICarrierConfigurationRepository carrierConfigurationRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<ServiceResponse<string>> CreateAsync(CreateCarrierConfigurationDTO dto)
        {
            var config = new CarrierConfiguration
            {
                CarrierId = dto.CarrierId,
                CarrierMaxDesi = dto.CarrierMaxDesi,
                CarrierMinDesi = dto.CarrierMinDesi,
                CarrierCost = dto.CarrierCost,
            };
            await _carrierConfigurationRepository.AddAsync(config);
            return new ServiceResponse<string>("Kargo konfigürasyonu başarıyla eklendi.");
        }

        public async Task<ServiceResponse<string>> DeleteAsync(int id)
        {
            var configuration = await _carrierConfigurationRepository.GetByIdAsync(id);
            if (configuration == null)
            {
                throw new NotFoundException($"{id} ID'li konfigürasyon bulunamadı.");
            }

            await _carrierConfigurationRepository.DeleteAsync(id);
            return new ServiceResponse<string>($"{id} ID'li konfigürasyon başarıyla silindi.");
        }

        public async Task<ServiceResponse<List<CarrierConfiguration>>> GetAllAsync()
        {
            var list = await _carrierConfigurationRepository.GetAllAsync();
            return new ServiceResponse<List<CarrierConfiguration>>(list);
        }

        public async Task<ServiceResponse<CarrierConfiguration>> GetByIdAsync(int id)
        {
            var configuration = await _carrierConfigurationRepository.GetByIdAsync(id);
            if (configuration == null)
            {
                throw new NotFoundException($"{id} ID'li konfigürasyon bulunamadı.");
            }
            return new ServiceResponse<CarrierConfiguration>(configuration);
        }

        public async Task<ServiceResponse<string>> UpdateAsync(int id, UpdateCarrierConfigurationDTO dto)
        {
            var configuration = await _carrierConfigurationRepository.GetByIdAsync(id);

            if (configuration == null)
            {
                throw new NotFoundException($"{id} ID'li konfigürasyon bulunamadı.");
            }

            configuration.CarrierMaxDesi = dto.CarrierMaxDesi;
            configuration.CarrierMinDesi = dto.CarrierMinDesi;
            configuration.CarrierCost = dto.CarrierCost;

            await _carrierConfigurationRepository.UpdateAsync(configuration);

            return new ServiceResponse<string>($"{id} ID'li konfigürasyon başarıyla güncellendi.");
        }
    }
}