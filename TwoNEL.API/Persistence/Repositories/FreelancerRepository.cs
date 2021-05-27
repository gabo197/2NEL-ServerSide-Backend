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
    public class FreelancerRepository : BaseRepository, IFreelancerRepository
    {
        public FreelancerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Freelancer freelancer)
        {
            await _context.Freelancers.AddAsync(freelancer);
        }

        public async Task<Freelancer> FindById(int id)
        {
            return await _context.Freelancers.FindAsync(id);
        }

        public async Task<IEnumerable<Freelancer>> ListAsync()
        {
            return await _context.Freelancers.ToListAsync();
        }

        public async Task<IEnumerable<Freelancer>> ListByUserIdAsync(int userId)
        {
            return await _context.Freelancers
                                        .Where(r => r.UserId == userId)
                                        .ToListAsync();
        }

        public void Update(Freelancer freelancer)
        {
            _context.Freelancers.Update(freelancer);
        }
    }
}
