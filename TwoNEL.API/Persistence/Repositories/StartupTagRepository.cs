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
    public class StartupTagRepository : BaseRepository, IStartupTagRepository
    {
        public StartupTagRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(StartupTag startupTag)
        {
            await _context.StartupTags.AddAsync(startupTag);
        }

        public async Task AssignStartupTag(int startupId, int tagId)
        {
            StartupTag startupTag = await FindByStartupIdAndTagId(startupId, tagId);
            if (startupTag == null)
            {
                startupTag = new StartupTag { StartupId = startupId, TagId = tagId };
                await AddAsync(startupTag);
            }
        }

        public async Task<StartupTag> FindByStartupIdAndTagId(int startupId, int tagId)
        {
            return await _context.StartupTags.FindAsync(startupId, tagId);
        }

        public async Task<IEnumerable<StartupTag>> ListAsync()
        {
            return await _context.StartupTags
                .Include(st => st.Startup)
                .Include(st => st.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<StartupTag>> ListByStartupIdAsync(int startupId)
        {
            return await _context.StartupTags
                .Where(st => st.StartupId == startupId)
                .Include(st => st.Startup)
                .Include(st => st.Tag)
                .ToListAsync();
        }

        public async Task<IEnumerable<StartupTag>> ListByTagIdAsync(int tagId)
        {
            return await _context.StartupTags
                .Where(st => st.TagId == tagId)
                .Include(st => st.Startup)
                .Include(st => st.Tag)
                .ToListAsync();
        }

        public void Remove(StartupTag startupTag)
        {
            _context.StartupTags.Remove(startupTag);
        }

        public async void UnassignStartupTag(int startupId, int tagId)
        {
            StartupTag startupTag = await _context.StartupTags.FindAsync(startupId, tagId);
            if (startupTag != null)
                Remove(startupTag);
        }
    }
}
