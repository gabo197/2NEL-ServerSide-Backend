using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> ListAsync();
        Task<IEnumerable<Profile>> ListByUserIdAsync(int userId);
        Task<Profile> FindById(int id);
        Task AddAsync(Profile profile);
        void Update(Profile profile);
    }
}
