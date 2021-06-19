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
    public class FavoriteStartupRepository : BaseRepository, IFavoriteStartupRepository
    {
        public FavoriteStartupRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(FavoriteStartup favoriteStartup)
        {
            await _context.FavoriteStartups.AddAsync(favoriteStartup);
        }

        public async Task AssignFavoriteStartup(int userId, int startupId)
        {
            FavoriteStartup favoriteStartup = await FindByUserIdAndStartupId(userId, startupId);
            if (favoriteStartup == null)
            {
                favoriteStartup = new FavoriteStartup { UserId = userId, StartupId = startupId };
                await AddAsync(favoriteStartup);
            }
        }

        public async Task<FavoriteStartup> FindByUserIdAndStartupId(int userId, int startupId)
        {
            return await _context.FavoriteStartups.FindAsync(userId, startupId);

        }

        public async Task<IEnumerable<FavoriteStartup>> ListAsync()
        {
            return await _context.FavoriteStartups
                .Include(fp => fp.Profile)
                .Include(fp => fp.Startup)
                .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteStartup>> ListByStartupIdAsync(int startupId)
        {
            return await _context.FavoriteStartups
               .Where(fp => fp.StartupId == startupId)
               .Include(fp => fp.Profile)
               .Include(fp => fp.Startup)
               .ToListAsync();
        }

        public async Task<IEnumerable<FavoriteStartup>> ListByUserIdAsync(int userId)
        {
            return await _context.FavoriteStartups
               .Where(fp => fp.UserId == userId)
               .Include(fp => fp.Profile)
               .Include(fp => fp.Startup)
               .ToListAsync();
        }

        public void Remove(FavoriteStartup favoriteStartup)
        {
            _context.FavoriteStartups.Remove(favoriteStartup);
        }

        public async void UnassignFavoriteStartup(int userId, int startupId)
        {
            FavoriteStartup favoriteStartup = await FindByUserIdAndStartupId(userId, startupId);
            if (favoriteStartup == null)
            {
                Remove(favoriteStartup);
            }
        }
    }
}
