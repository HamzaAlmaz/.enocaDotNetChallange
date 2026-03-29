using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CarrierConfigurationRepository : ICarrierConfigurationRepository
    {
        private readonly AppDbContext _context;

        public CarrierConfigurationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(CarrierConfiguration carrierConfiguration)
        {
            await _context.CarrierConfigurations.AddAsync(carrierConfiguration);
            await _context.SaveChangesAsync();
            return carrierConfiguration.CarrierConfigurationId;
        }

        public async Task<CarrierConfiguration?> GetByIdAsync(int id)
        {
            return await _context.CarrierConfigurations.FindAsync(id);
        }

        public async Task<List<CarrierConfiguration>> GetAllAsync()
        {
            return await _context.CarrierConfigurations.ToListAsync();
        }

        public async Task<List<CarrierConfiguration>> GetByCarrierIdAsync(int carrierId)
        {
            return await _context.CarrierConfigurations
                .Where(cc => cc.CarrierId == carrierId)
                .ToListAsync();
        }

        public async Task UpdateAsync(CarrierConfiguration carrierConfiguration)
        {
            _context.CarrierConfigurations.Update(carrierConfiguration);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var config = await _context.CarrierConfigurations.FindAsync(id);
            if (config == null)
                return false;

            _context.CarrierConfigurations.Remove(config);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}