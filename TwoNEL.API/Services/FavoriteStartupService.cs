using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Repositories;
using TwoNEL.API.Domain.Services;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Services
{
    public class FavoriteStartupService : IFavoriteStartupService
    {
        private readonly IFavoriteStartupRepository favoriteStartupRepository;
        private readonly IUnitOfWork unitOfWork;

        public FavoriteStartupService(IFavoriteStartupRepository favoriteStartupRepository, IUnitOfWork unitOfWork)
        {
            this.favoriteStartupRepository = favoriteStartupRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FavoriteStartupResponse> AssignFavoriteStartupAsync(int userId, int startupId)
        {
            try
            {
                await favoriteStartupRepository.AssignFavoriteStartup(userId, startupId);
                await unitOfWork.CompleteAsync();

                FavoriteStartup favoriteStartup = await favoriteStartupRepository.FindByUserIdAndStartupId(userId, startupId);

                return new FavoriteStartupResponse(favoriteStartup);
            }
            catch (Exception ex)
            {
                return new FavoriteStartupResponse($"An error ocurred while assigning Favorite Startup to User: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FavoriteStartup>> ListAsync()
        {
            return await favoriteStartupRepository.ListAsync();
        }

        public async Task<IEnumerable<FavoriteStartup>> ListByStartupIdAsync(int startupId)
        {
            return await favoriteStartupRepository.ListByStartupIdAsync(startupId);
        }

        public async Task<IEnumerable<FavoriteStartup>> ListByUserIdAsync(int userId)
        {
            return await favoriteStartupRepository.ListByUserIdAsync(userId);
        }

        public async Task<FavoriteStartupResponse> UnassignFavoriteStartupAsync(int userId, int startupId)
        {
            try
            {
                FavoriteStartup favoriteStartup = await favoriteStartupRepository.FindByUserIdAndStartupId(userId, startupId);
                favoriteStartupRepository.UnassignFavoriteStartup(userId, startupId);
                await unitOfWork.CompleteAsync();

                return new FavoriteStartupResponse(favoriteStartup);
            }
            catch (Exception ex)
            {
                return new FavoriteStartupResponse($"An error ocurred while unassigning Favorite Startup to User: {ex.Message}");
            }
        }
    }
}
