using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommmerce.Data.IRepositories
{
    public interface GenericRepository<Tentity> where Tentity : class

    { 
    Task<Tentity> CreateAsync(Tentity model);
    Task<List<Tentity>> GetAllAsync(Predicate<Tentity> predicate = null);
    Task<Tentity> GetByIdAsync(Predicate<Tentity> predicate);
    Task<bool> DeleteAysnc(Predicate<Tentity> predicate);
    Task<Tentity> UpdateAsync(Predicate<Tentity> predicate, Tentity model);

    }
}
