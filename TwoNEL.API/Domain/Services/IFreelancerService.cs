using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IFreelancerService
    {
        Task<IEnumerable<Freelancer>> ListAsync();
        Task<FreelancerResponse> GetByIdAsync(int id);
        Task<FreelancerResponse> SaveAsync(Freelancer freelancer);
        Task<FreelancerResponse> UpdateAsync(int id, Freelancer freelancer);
    }
}
