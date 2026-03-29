using Core.Application.Interfaces.Repositories;
using Core.Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CarrierRepository : ICarrierRepository
    {
        private readonly AppDbContext _context;

        public CarrierRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Carrier carrier)
        {
            await _context.Carriers.AddAsync(carrier);
            await _context.SaveChangesAsync();
            return carrier.CarrierId;
        }
        public async Task<Carrier?> GetByNameAsync(string name)
        {
            return await _context.Carriers.FirstOrDefaultAsync(x=> x.CarrierName==name);
        }
        public async Task<Carrier?> GetByIdAsync(int id)
        {
            return await _context.Carriers.FindAsync(id);
        }
            public async Task<List<Carrier>> GetAllAsync()
            {
                return await _context.Carriers.Where(x => x.CarrierIsActive).ToListAsync();
            }
        public async Task<bool> UpdateAsync(Carrier carrier)
        {
            _context.Carriers.Update(carrier);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var carrier = await _context.Carriers.FindAsync(id);
            carrier.CarrierIsActive=false;
            _context.Carriers.Update(carrier);
            return await _context.SaveChangesAsync() > 0;
        }


    }
}
