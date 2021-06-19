using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IFavoriteStartupService
    {
        Task<IEnumerable<FavoriteStartup>> ListAsync();
        Task<IEnumerable<FavoriteStartup>> ListByUserIdAsync(int userId);
        Task<IEnumerable<FavoriteStartup>> ListByStartupIdAsync(int startupId);
        Task<FavoriteStartupResponse> AssignFavoriteStartupAsync(int userId, int startupId);
        Task<FavoriteStartupResponse> UnassignFavoriteStartupAsync(int userId, int startupId);
    }
}
