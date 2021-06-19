using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IFavoriteProfileService
    {
        Task<IEnumerable<FavoriteProfile>> ListAsync();
        Task<IEnumerable<FavoriteProfile>> ListByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteProfile>> ListByFavoriteIdAsync(int favoriteId);
        Task<FavoriteProfileResponse> AssignFavoriteProfileAsync(int userId, int favoriteId);
        Task<FavoriteProfileResponse> UnassignFavoriteProfileAsync(int userId, int favoriteId);
    }
}
