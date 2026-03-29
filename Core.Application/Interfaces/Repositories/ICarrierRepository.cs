using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Application.Interfaces.Repositories
{
    public interface ICarrierRepository
    {
        Task<int> AddAsync(Carrier carrier);

        Task<Carrier?> GetByIdAsync(int id);
        Task<Carrier?> GetByNameAsync(string name);
        Task<List<Carrier>> GetAllAsync();

        Task<bool> UpdateAsync(Carrier carrier);

        Task<bool> DeleteAsync(int id);
    }
}
    