using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IRequestRepository
    {
        Task<IEnumerable<Request>> ListByUserIdAsync(int userId);
        Task<Request> FindById(int id);
        Task AddAsync(Request request);
        void Update(Request request);
        void Remove(Request request);
    }
}
