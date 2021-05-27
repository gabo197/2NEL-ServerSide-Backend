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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProfileService(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
        {
            this.profileRepository = profileRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProfileResponse> GetByIdAsync(int id)
        {
            var existingProfile = await profileRepository.FindById(id);

            if (existingProfile == null)
                return new ProfileResponse("Profile not found");
            return new ProfileResponse(existingProfile);
        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await profileRepository.ListAsync();
        }

        public async Task<ProfileResponse> SaveAsync(Profile profile)
        {
            try
            {
                await profileRepository.AddAsync(profile);
                await unitOfWork.CompleteAsync();

                return new ProfileResponse(profile);
            }
            catch (Exception ex)
            {
                return new ProfileResponse($"An error ocurred while saving the profile: {ex.Message}");
            }
        }

        public async Task<ProfileResponse> UpdateAsync(int id, Profile profile)
        {
            var existingProfile = await profileRepository.FindById(id);

            if (existingProfile == null)
                return new ProfileResponse("Profile not found");

            existingProfile.FirstName = profile.FirstName;
            existingProfile.LastName = profile.LastName;
            existingProfile.Portfolio = profile.Portfolio;

            try
            {
                profileRepository.Update(existingProfile);
                await unitOfWork.CompleteAsync();

                return new ProfileResponse(existingProfile);
            }
            catch (Exception ex)
            {
                return new ProfileResponse($"An error ocurred while updating the profile: {ex.Message}");
            }
        }
    }
}
