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
    public class InvestorRepository : BaseRepository, IInvestorRepository
    {
        public InvestorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Investor investor)
        {
            await _context.Investors.AddAsync(investor);
        }

        public async Task<Investor> FindById(int id)
        {
            return await _context.Investors.FindAsync(id);
        }

        public async Task<IEnumerable<Investor>> ListAsync()
        {
            return await _context.Investors.ToListAsync();
        }

        public async Task<IEnumerable<Investor>> ListByUserIdAsync(int userId)
        {
            return await _context.Investors
                            .Where(r => r.UserId == userId)
                            .ToListAsync();
        }

        public void Update(Investor investor)
        {
            _context.Investors.Update(investor);
        }
    }
}
