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
    public class StartupTagService : IStartupTagService
    {
        private readonly IStartupTagRepository startupTagRepository;
        private readonly IUnitOfWork unitOfWork;

        public StartupTagService(IStartupTagRepository startupTagRepository, IUnitOfWork unitOfWork)
        {
            this.startupTagRepository = startupTagRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<StartupTagResponse> AssignStartupTagAsync(int startupId, int tagId)
        {
            try
            {
                await startupTagRepository.AssignStartupTag(startupId, tagId);
                await unitOfWork.CompleteAsync();

                StartupTag startupTag = await startupTagRepository.FindByStartupIdAndTagId(startupId, tagId);

                return new StartupTagResponse(startupTag);
            }
            catch (Exception ex)
            {
                return new StartupTagResponse($"An error ocurred while assigning Tag to Startup: {ex.Message}");
            }
        }

        public async Task<IEnumerable<StartupTag>> ListAsync()
        {
            return await startupTagRepository.ListAsync();
        }

        public async Task<IEnumerable<StartupTag>> ListByStartupIdAsync(int startupId)
        {
            return await startupTagRepository.ListByStartupIdAsync(startupId);
        }

        public async Task<IEnumerable<StartupTag>> ListByTagIdAsync(int tagId)
        {
            return await startupTagRepository.ListByTagIdAsync(tagId);
        }

        public async Task<StartupTagResponse> UnassignStartupTagAsync(int startupId, int tagId)
        {
            try
            {
                StartupTag startupTag = await startupTagRepository.FindByStartupIdAndTagId(startupId, tagId);
                startupTagRepository.UnassignStartupTag(startupId, tagId);
                await unitOfWork.CompleteAsync();

                return new StartupTagResponse(startupTag);
            }
            catch (Exception ex)
            {
                return new StartupTagResponse($"An error ocurred while unassigning Tag to Startup: {ex.Message}");
            }
        }
    }
}
