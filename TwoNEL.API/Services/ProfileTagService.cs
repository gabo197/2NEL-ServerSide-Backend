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
        private readonly IProfileTagRepository userTagRepository;
        private readonly IUnitOfWork unitOfWork;

        public ProfileTagService(IProfileTagRepository userTagRepository, IUnitOfWork unitOfWork)
        {
            this.userTagRepository = userTagRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserTagResponse> AssignUserTagAsync(int userId, int tagId)
        {
            try
            {
                await userTagRepository.AssignUserTag(userId, tagId);
                await unitOfWork.CompleteAsync();

                ProfileTag userTag = await userTagRepository.FindByUserIdAndTagId(userId, tagId);

                return new UserTagResponse(userTag);
            }
            catch (Exception ex)
            {
                return new UserTagResponse($"An error ocurred while assigning Tag to User: {ex.Message}");
            }
        }

        public async Task<IEnumerable<ProfileTag>> ListAsync()
        {
            return await userTagRepository.ListAsync();
        }

        public async Task<IEnumerable<ProfileTag>> ListByTagIdAsync(int tagId)
        {
            return await userTagRepository.ListByTagIdAsync(tagId);
        }

        public async Task<IEnumerable<ProfileTag>> ListByUserIdAsync(int userId)
        {
            return await userTagRepository.ListByUserIdAsync(userId);
        }

        public async Task<UserTagResponse> UnassignUserTagAsync(int userId, int tagId)
        {
            try
            {
                ProfileTag userTag = await userTagRepository.FindByUserIdAndTagId(userId, tagId);
                userTagRepository.UnassignUserTag(userId, tagId);
                await unitOfWork.CompleteAsync();

                return new UserTagResponse(userTag);
            }
            catch (Exception ex)
            {
                return new UserTagResponse($"An error ocurred while unassigning Tag to User: {ex.Message}");
            }
        }
    }
}
