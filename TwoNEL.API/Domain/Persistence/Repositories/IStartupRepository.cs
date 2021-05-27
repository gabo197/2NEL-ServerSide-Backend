using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IStartupRepository
    {
        Task<IEnumerable<Models.Startup>> ListAsync();
        Task AddAsync(Models.Startup startup);
        Task<Models.Startup> FindById(int id);
        void Update(Models.Startup startup);
        void Remove(Models.Startup startup);
    }
}
