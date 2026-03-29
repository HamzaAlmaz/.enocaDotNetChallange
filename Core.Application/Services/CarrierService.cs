using Core.Application.DTOs.carrier;
using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.Services;
using Core.Application.Wrappers;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;

        public CarrierService(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        public async Task<ServiceResponse<string>> AddAsync(CreateCarrierDTO dto)
        {
            var carrier_= await _carrierRepository.GetByNameAsync(dto.CarrierName);
            if (carrier_ != null) 
            {
                throw new ApiException($"{carrier_.CarrierName} isimli kargo firması bulunmaktadir.");
            }
            var carrier = new Carrier
            {
                CarrierPlusDesiCost = dto.CarrierPlusDesiCost,
                CarrierName = dto.CarrierName,
                CarrierIsActive = true
            };

            await _carrierRepository.AddAsync(carrier);

            return new ServiceResponse<string>("Kargo firması başarıyla eklendi.");
        }

        public async Task<ServiceResponse<string>> DeleteAsync(int id)
        {
            var carrier = await _carrierRepository.GetByIdAsync(id);

            if (carrier == null)
            {
                throw new NotFoundException($"{id} ID'li kargo firması bulunamadı.");
            }

            await _carrierRepository.DeleteAsync(id);
            return new ServiceResponse<string>("Silme işlemi başarılı.");
        }

        public async Task<ServiceResponse<List<Carrier>>> GetAllAsync()
        {
            var carriers = await _carrierRepository.GetAllAsync();
            return new ServiceResponse<List<Carrier>>(carriers);
        }

        public async Task<ServiceResponse<Carrier>> GetByIdAsync(int id)
        {
            var carrier = await _carrierRepository.GetByIdAsync(id);

            if (carrier == null)
            {
                throw new NotFoundException($"{id} ID'li kargo firması bulunamadı.");
            }

            return new ServiceResponse<Carrier>(carrier);
        }

        public async Task<ServiceResponse<string>> UpdateAsync(int carrierId, UpdateCarrierDTO dto)
        {
            var carrier = await _carrierRepository.GetByIdAsync(carrierId);

            if (carrier == null)
            {
                throw new NotFoundException($"{carrierId} ID'li kargo firması bulunamadı.");
            }

            carrier.CarrierName = dto.CarrierName;
            carrier.CarrierPlusDesiCost = dto.CarrierPlusDesiCost;
            carrier.CarrierIsActive = dto.CarrierIsActive;

            await _carrierRepository.UpdateAsync(carrier);
            return new ServiceResponse<string>("Kargo firması başarıyla güncellendi.");
        }
    }
}