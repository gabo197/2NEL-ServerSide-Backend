using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> ListAsync();
        Task<IEnumerable<Request>> ListByUserIdAsync(int userId);
        Task<RequestResponse> GetByIdAsync(int id);
        Task<RequestResponse> SaveAsync(Request request);
        Task<RequestResponse> UpdateAsync(int id, Request request);
        Task<RequestResponse> DeleteAsync(int id);
    }
}
