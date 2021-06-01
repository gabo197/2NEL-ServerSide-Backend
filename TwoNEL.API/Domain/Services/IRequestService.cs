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
        Task<IEnumerable<Request>> ListByUserIdAsync(int userId);
        Task<RequestResponse> GetByIdAsync(int userId, int requestId);
        Task<RequestResponse> SaveAsync(int userId, Request request);
        Task<RequestResponse> UpdateAsync(int userId, int requestId, Request request);
        Task<RequestResponse> DeleteAsync(int userId, int requestId);
    }
}
