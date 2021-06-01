using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IEnterpriseRepository
    {
        Task<IEnumerable<Enterprise>> ListAsync();
        Task<Enterprise> FindById(int id);
        Task AddAsync(int userId, Enterprise enterprise);
        void Update(Enterprise enterprise);
        void Remove(Enterprise enterprise);
    }
}