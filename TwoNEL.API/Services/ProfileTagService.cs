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
    public class ProfileTagService : IProfileTagService
    {
        private readonly IProfileTagRepository profileTagRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProfileTagService(IProfileTagRepository profileTagRepository, IUnitOfWork unitOfWork)
        {
            this.profileTagRepository = profileTagRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ProfileTagResponse> AssignProfileTagAsync(int userId, int tagId)
        {
            try
            {
                await profileTagRepository.AssignProfileTag(userId, tagId);
                await unitOfWork.CompleteAsync();

                ProfileTag profileTag = await profileTagRepository.FindByUserIdAndTagId(userId, tagId);

                return new ProfileTagResponse(profileTag);
            }
            catch (Exception ex)
            {
                return new ProfileTagResponse($"An error ocurred while assigning Tag to User: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProfileTag>> ListAsync()
        {
            return await profileTagRepository.ListAsync();
        }

        public async Task<IEnumerable<ProfileTag>> ListByTagIdAsync(int tagId)
        {
            return await profileTagRepository.ListByTagIdAsync(tagId);
        }

        public async Task<IEnumerable<ProfileTag>> ListByUserIdAsync(int userId)
        {
            return await profileTagRepository.ListByUserIdAsync(userId);
        }

        public async Task<ProfileTagResponse> UnassignProfileTagAsync(int userId, int tagId)
        {
            try
            {
                ProfileTag profileTag = await profileTagRepository.FindByUserIdAndTagId(userId, tagId);
                profileTagRepository.UnassignProfileTag(userId, tagId);
                await unitOfWork.CompleteAsync();

                return new ProfileTagResponse(profileTag);
            }
            catch (Exception ex)
            {
                return new ProfileTagResponse($"An error ocurred while unassigning Tag to User: {ex.Message}");
            }
        }
    }
}
