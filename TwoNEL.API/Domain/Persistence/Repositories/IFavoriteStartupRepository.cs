using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IFavoriteStartupRepository
    {
        Task<IEnumerable<FavoriteStartup>> ListAsync();
        Task<IEnumerable<FavoriteStartup>> ListByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteStartup>> ListByStartupIdAsync(int startupId);
        Task<FavoriteStartup> FindByUserIdAndStartupId(int userId, int startupId);
        Task AddAsync(FavoriteStartup favoriteStartup);
        void Remove(FavoriteStartup favoriteStartup);
        Task AssignFavoriteStartup(int userId, int startupId);
        void UnassignFavoriteStartup(int userId, int startupId);
    }
}
