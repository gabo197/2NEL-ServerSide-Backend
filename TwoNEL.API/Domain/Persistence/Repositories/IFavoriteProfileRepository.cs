using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IFavoriteProfileRepository
    {
        Task<IEnumerable<FavoriteProfile>> ListAsync();
        Task<IEnumerable<FavoriteProfile>> ListByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteProfile>> ListByFavoriteIdAsync(int favoriteId);
        Task<FavoriteProfile> FindByUserIdAndFavoriteId(int userId, int favoriteId);
        Task AddAsync(FavoriteProfile favoriteProfile);
        void Remove(FavoriteProfile favoriteProfile);
        Task AssignFavoriteProfile(int userId, int favoriteId);
        void UnassignFavoriteProfile(int userId, int favoriteId);
    }
}
