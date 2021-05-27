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
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public async Task<Profile> FindById(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByUserIdAsync(int userId)
        {
            return await _context.Profiles
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public void Update(Profile profile)
        {
            _context.Profiles.Update(profile);
        }
    }
}
