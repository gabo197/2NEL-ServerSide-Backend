using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IInvestorRepository
    {
        Task<IEnumerable<Investor>> ListAsync();
        Task<IEnumerable<Investor>> ListByUserIdAsync(int userId);
        Task<Investor> FindById(int id);
        Task AddAsync(Investor investor);
        void Update(Investor investor);
    }
}
