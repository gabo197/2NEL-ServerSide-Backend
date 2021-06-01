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
    public class StartupRepository : BaseRepository, IStartupRepository
    {
        public StartupRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Domain.Models.Startup startup)
        {
            await _context.Startups.AddAsync(startup);
        }

        public async Task<Domain.Models.Startup> FindById(int id)
        {
            return await _context.Startups.FindAsync(id);
        }

        public async Task<IEnumerable<Domain.Models.Startup>> ListByEnterpriseIdAsync(int enterpriseId)
        {
            return await _context.Startups
                .Where(s => s.EnterpriseId == enterpriseId)
                .Include(s => s.Enterprise)
                .ToListAsync();
        }

        public void Remove(Domain.Models.Startup startup)
        {
            _context.Startups.Remove(startup);
        }

        public void Update(Domain.Models.Startup startup)
        {
            _context.Startups.Update(startup);
        }
    }
}
