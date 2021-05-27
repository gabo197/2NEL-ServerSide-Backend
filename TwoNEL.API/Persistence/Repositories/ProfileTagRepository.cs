using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Persistence.Contexts;
using TwoNEL.API.Domain.Persistence.Repositories;

namespace TwoNEL.API.Persistence.Repositories
{
    public class ProfileTagRepository : BaseRepository, IProfileTagRepository
    {
        public ProfileTagRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(ProfileTag profileTag)
        {
            await _context.ProfileTags.AddAsync(profileTag);
        }

        public async Task AssignUserTag(int userId, int tagId)
        {
            ProfileTag profileTag = await FindByUserIdAndTagId(userId, tagId);
            if (profileTag == null)
            {
                profileTag = new ProfileTag { UserId = userId, TagId = tagId };
                await AddAsync(profileTag);
            }
        }

        public async Task<ProfileTag> FindByUserIdAndTagId(int userId, int tagId)
        {
            return await _context.ProfileTags.FindAsync(userId, tagId);
        }

        public async Task<IEnumerable<ProfileTag>> ListAsync()
        {
            return await _context.ProfileTags
                .Include(pt => pt.Profile)
                .Include(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProfileTag>> ListByTagIdAsync(int tagId)
        {
            return await _context.ProfileTags
                .Where(pt => pt.TagId == tagId)
                .Include(pt => pt.Profile)
                .Include(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProfileTag>> ListByUserIdAsync(int userId)
        {
            return await _context.ProfileTags
                .Where(pt => pt.UserId == userId)
                .Include(pt => pt.Profile)
                .Include(pt => pt.Tag)
                .ToListAsync();
        }

        public void Remove(ProfileTag profileTag)
        {
            _context.ProfileTags.Remove(profileTag);
        }

        public async void UnassignUserTag(int userId, int tagId)
        {
            ProfileTag profileTag = await _context.ProfileTags.FindAsync(userId, tagId);
            if (profileTag != null)
                Remove(profileTag);
        }
    }
}
