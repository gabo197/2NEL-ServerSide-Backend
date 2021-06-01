using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IInvestorService
    {
        Task<IEnumerable<Investor>> ListAsync();
        Task<InvestorResponse> GetByIdAsync(int id);
        Task<InvestorResponse> SaveAsync(int userId, Investor investor);
        Task<InvestorResponse> UpdateAsync(int id, Investor investor);
    }
}
