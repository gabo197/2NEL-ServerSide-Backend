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
    public class FavoriteProfileService : IFavoriteProfileService
    {
        private readonly IFavoriteProfileRepository favoriteProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public FavoriteProfileService(IFavoriteProfileRepository favoriteProfileRepository, IUnitOfWork unitOfWork)
        {
            this.favoriteProfileRepository = favoriteProfileRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<FavoriteProfileResponse> AssignFavoriteProfileAsync(int userId, int favoriteId)
        {
            try
            {
                await favoriteProfileRepository.AssignFavoriteProfile(userId, favoriteId);
                await unitOfWork.CompleteAsync();

                FavoriteProfile favoriteProfile = await favoriteProfileRepository.FindByUserIdAndFavoriteId(userId, favoriteId);

                return new FavoriteProfileResponse(favoriteProfile);
            }
            catch (Exception ex)
            {
                return new FavoriteProfileResponse($"An error ocurred while assigning Favorite to User: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FavoriteProfile>> ListAsync()
        {
            return await favoriteProfileRepository.ListAsync();
        }

        public async Task<IEnumerable<FavoriteProfile>> ListByFavoriteIdAsync(int favoriteId)
        {
            return await favoriteProfileRepository.ListByFavoriteIdAsync(favoriteId);

        }

        public async Task<IEnumerable<FavoriteProfile>> ListByUserIdAsync(int userId)
        {
            return await favoriteProfileRepository.ListByUserIdAsync(userId);
        }

        public async Task<FavoriteProfileResponse> UnassignFavoriteProfileAsync(int userId, int favoriteId)
        {
            try
            {
                FavoriteProfile favoriteProfile = await favoriteProfileRepository.FindByUserIdAndFavoriteId(userId, favoriteId);
                favoriteProfileRepository.UnassignFavoriteProfile(userId, favoriteId);
                await unitOfWork.CompleteAsync();

                return new FavoriteProfileResponse(favoriteProfile);
            }
            catch (Exception ex)
            {
                return new FavoriteProfileResponse($"An error ocurred while unassigning Favorite to User: {ex.Message}");
            }
        }
    }
}
