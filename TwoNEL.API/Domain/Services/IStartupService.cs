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
        Task<IEnumerable<Models.Startup>> ListAsync();
        Task<StartupResponse> GetByIdAsync(int id);
        Task<StartupResponse> SaveAsync(Models.Startup startup);
        Task<StartupResponse> UpdateAsync(int id, Models.Startup startup);
        Task<StartupResponse> DeleteAsync(int id);
    }
}
