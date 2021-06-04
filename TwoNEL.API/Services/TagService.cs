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
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IStartupTagRepository startupTagRepository;
        private readonly IProfileTagRepository profileTagRepository;
        private readonly IUnitOfWork unitOfWork;

        public TagService(ITagRepository tagRepository, IStartupTagRepository startupTagRepository, IProfileTagRepository profileTagRepository, IUnitOfWork unitOfWork)
        {
            this.tagRepository = tagRepository;
            this.startupTagRepository = startupTagRepository;
            this.profileTagRepository = profileTagRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TagResponse> DeleteAsync(int id)
        {
            var existingTag = await tagRepository.FindById(id);

            if (existingTag == null)
                return new TagResponse("Tag not found");

            try
            {
                tagRepository.Remove(existingTag);
                await unitOfWork.CompleteAsync();

                return new TagResponse(existingTag);

            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while deleting tag: {ex.Message}");
            }
        }

        public async Task<TagResponse> GetByIdAsync(int id)
        {
            var existingTag = await tagRepository.FindById(id);

            if (existingTag == null)
                return new TagResponse("Tag not found");

            return new TagResponse(existingTag);
        }

        public async Task<IEnumerable<Tag>> ListAsync()
        {
            return await tagRepository.ListAsync();
        }

        public async Task<IEnumerable<Tag>> ListByStartupIdAsync(int startupId)
        {
            var startupTags = await startupTagRepository.ListByStartupIdAsync(startupId);
            var tags = startupTags.Select(st => st.Tag).ToList();
            return tags;
        }

        public async Task<IEnumerable<Tag>> ListByUserIdAsync(int userId)
        {
            var profileTags = await profileTagRepository.ListByUserIdAsync(userId);
            var tags = profileTags.Select(ut => ut.Tag).ToList();
            return tags;
        }

        public async Task<TagResponse> SaveAsync(Tag tag)
        {
            try
            {
                await tagRepository.AddAsync(tag);
                await unitOfWork.CompleteAsync();

                return new TagResponse(tag);
            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while saving tag: {ex.Message}");
            }
        }

        public async Task<TagResponse> UpdateAsync(int id, Tag tag)
        {
            var existingTag = await tagRepository.FindById(id);

            if (existingTag == null)
                return new TagResponse("Tag not found");

            existingTag.Name = tag.Name;

            try
            {
                tagRepository.Update(tag);
                await unitOfWork.CompleteAsync();

                return new TagResponse(tag);
            }
            catch (Exception ex)
            {
                return new TagResponse($"An error ocurred while updating tag: {ex.Message}");
            }
        }
    }
}
