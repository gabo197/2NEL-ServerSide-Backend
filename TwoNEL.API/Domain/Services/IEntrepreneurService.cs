using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IEntrepreneurService
    {
        Task<IEnumerable<Entrepreneur>> ListAsync();
        Task<EntrepreneurResponse> GetByIdAsync(int id);
        Task<EntrepreneurResponse> SaveAsync(int userId, Entrepreneur entrepreneur);
        Task<EntrepreneurResponse> UpdateAsync(int id, Entrepreneur entrepreneur);
    }
}
