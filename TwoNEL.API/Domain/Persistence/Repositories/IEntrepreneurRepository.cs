using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IEntrepreneurRepository
    {
        Task<IEnumerable<Entrepreneur>> ListAsync();
        Task<IEnumerable<Entrepreneur>> ListByUserIdAsync(int userId);
        Task<Entrepreneur> FindById(int id);
        Task AddAsync(Entrepreneur entrepreneur);
        void Update(Entrepreneur entrepreneur);
    }
}
