using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Contexts;
using TwoNEL.API.Domain.Persistence.Repositories;

namespace TwoNEL.API.Persistence.Repositories
{
    public class RequestRepository : BaseRepository, IRequestRepository
    {
        public RequestRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
        }

        public async Task<Request> FindById(int id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<IEnumerable<Request>> ListAsync()
        {
            return await _context.Requests.Include(r => r.Profile).ToListAsync();
        }

        public async Task<IEnumerable<Request>> ListByUserIdAsync(int userId)
        {
            return await _context.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.Profile)
                .ToListAsync();
        }

        public void Remove(Request request)
        {
            _context.Requests.Remove(request);
        }

        public void Update(Request request)
        {
            _context.Requests.Update(request);
        }
    }
}
