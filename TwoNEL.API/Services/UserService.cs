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
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IFavoriteProfileRepository favoriteProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IFavoriteProfileRepository favoriteProfileRepository)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.favoriteProfileRepository = favoriteProfileRepository;
        }

        public async Task<UserResponse> DeleteAsync(int id)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            try
            {
                userRepository.Remove(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");
            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await userRepository.ListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByFavoriteIdAsync(int favoriteId)
        {
            var favoriteProfiles = await favoriteProfileRepository.ListByFavoriteIdAsync(favoriteId);
            var profiles = favoriteProfiles.Select(st => st.Favorite).ToList();
            return profiles;
        }

        public async Task<IEnumerable<Profile>> ListByUserIdAsync(int userId)
        {
            var favoriteProfiles = await favoriteProfileRepository.ListByUserIdAsync(userId);
            var profiles = favoriteProfiles.Select(st => st.Profile).ToList();
            return profiles;
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await userRepository.AddAsync(user);
                await unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int id, User user)
        {
            var existingUser = await userRepository.FindById(id);

            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            try
            {
                userRepository.Update(existingUser);
                await unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }
        }
    }
}
