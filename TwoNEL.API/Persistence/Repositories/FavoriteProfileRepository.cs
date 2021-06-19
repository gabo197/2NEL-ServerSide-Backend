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
    public class FavoriteProfileRepository : BaseRepository, IFavoriteProfileRepository
    {
        public FavoriteProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(FavoriteProfile favoriteProfile)
        {
            await _context.FavoriteProfiles.AddAsync(favoriteProfile);
        }

        public async Task AssignFavoriteProfile(int userId, int favoriteId)
        {
            FavoriteProfile favoriteProfile = await FindByUserIdAndFavoriteId(userId, favoriteId);
            if (favoriteProfile == null)
            {
                favoriteProfile = new FavoriteProfile { UserId = userId, FavoriteId = favoriteId };
                await AddAsync(favoriteProfile);
            }
        }

        public async Task<FavoriteProfile> FindByUserIdAndFavoriteId(int userId, int favoriteId)
        {
            return await _context.FavoriteProfiles.FindAsync(userId, favoriteId);
        }

        public async Task<IEnumerable<FavoriteProfile>> ListAsync()
        {
            return await _context.FavoriteProfiles
                .Include(fp => fp.Profile)
                .Include(fp => fp.Favorite)
                .ToListAsync();
        }

        public async  Task<IEnumerable<FavoriteProfile>> ListByFavoriteIdAsync(int favoriteId)
        {
            return await _context.FavoriteProfiles
                .Where(fp => fp.FavoriteId == favoriteId)
                .Include(fp => fp.Profile)
                .Include(fp => fp.Favorite)
                .ToListAsync();
        }

        public async  Task<IEnumerable<FavoriteProfile>> ListByUserIdAsync(int userId)
        {
            return await _context.FavoriteProfiles
                .Where(fp => fp.UserId == userId)
                .Include(fp => fp.Profile)
                .Include(fp => fp.Favorite)
                .ToListAsync();
        }

        public void Remove(FavoriteProfile favoriteProfile)
        {
            _context.FavoriteProfiles.Remove(favoriteProfile);
        }

        public async  void UnassignFavoriteProfile(int userId, int favoriteId)
        {
            FavoriteProfile favoriteProfile = await FindByUserIdAndFavoriteId(userId, favoriteId);
            if (favoriteProfile == null)
            {
                Remove(favoriteProfile);
            }
        }
    }
}
