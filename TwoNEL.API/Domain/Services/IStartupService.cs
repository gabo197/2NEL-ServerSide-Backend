using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IStartupService
    {
        Task<IEnumerable<Models.Startup>> ListByEnterpriseIdAsync(int enterpriseId);
        Task<StartupResponse> GetByIdAsync(int enterpriseId, int id);
        Task<StartupResponse> SaveAsync(int enterpriseId, Models.Startup startup);
        Task<StartupResponse> UpdateAsync(int enterpriseId, int id, Models.Startup startup);
        Task<StartupResponse> DeleteAsync(int enterpriseId, int id);
    }
}
