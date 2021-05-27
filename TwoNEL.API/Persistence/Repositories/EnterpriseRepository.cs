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
    public class EnterpriseRepository : BaseRepository, IEnterpriseRepository
    {
        public EnterpriseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Enterprise enterprise)
        {
            await _context.Enterprises.AddAsync(enterprise);
        }

        public async Task<Enterprise> FindById(int id)
        {
            return await _context.Enterprises.FindAsync(id);
        }

        public async Task<IEnumerable<Enterprise>> ListAsync()
        {
            return await _context.Enterprises.ToListAsync();
        }

        public void Remove(Enterprise enterprise)
        {
            _context.Enterprises.Remove(enterprise);
        }

        public void Update(Enterprise enterprise)
        {
            _context.Enterprises.Update(enterprise);
        }
    }
}
