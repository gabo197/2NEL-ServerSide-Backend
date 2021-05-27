using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IFreelancerRepository
    {
        Task<IEnumerable<Freelancer>> ListAsync();
        Task<IEnumerable<Freelancer>> ListByUserIdAsync(int userId);
        Task<Freelancer> FindById(int id);
        Task AddAsync(Freelancer freelancer);
        void Update(Freelancer freelancer);
    }
}
