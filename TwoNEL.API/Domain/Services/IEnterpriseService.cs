using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IEnterpriseService
    {
        Task<IEnumerable<Enterprise>> ListAsync();
        Task<EnterpriseResponse> GetByIdAsync(int id);
        Task<EnterpriseResponse> SaveAsync(Enterprise enterprise);
        Task<EnterpriseResponse> UpdateAsync(int id, Enterprise enterprise);
        Task<EnterpriseResponse> DeleteAsync(int id);
    }
}