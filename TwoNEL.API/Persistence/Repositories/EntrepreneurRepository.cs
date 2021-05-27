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
    public class EntrepreneurRepository : BaseRepository, IEntrepreneurRepository
    {
        public EntrepreneurRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Entrepreneur entrepreneur)
        {
            await _context.Entrepreneurs.AddAsync(entrepreneur);
        }

        public async Task<Entrepreneur> FindById(int id)
        {
            return await _context.Entrepreneurs.FindAsync(id);
        }

        public async Task<IEnumerable<Entrepreneur>> ListAsync()
        {
            return await _context.Entrepreneurs.ToListAsync();
        }

        public async Task<IEnumerable<Entrepreneur>> ListByUserIdAsync(int userId)
        {
            return await _context.Entrepreneurs
                                   .Where(r => r.UserId == userId)
                                   .ToListAsync();
        }

        public void Update(Entrepreneur entrepreneur)
        {
            _context.Entrepreneurs.Update(entrepreneur);
        }
    }
}
