using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IStartupTagRepository
    {
        Task<IEnumerable<StartupTag>> ListAsync();
        Task<IEnumerable<StartupTag>> ListByStartupIdAsync(int startupId);
        Task<IEnumerable<StartupTag>> ListByTagIdAsync(int tagId);
        Task<StartupTag> FindByStartupIdAndTagId(int startupId, int tagId);
        Task AddAsync(StartupTag startupTag);
        void Remove(StartupTag startupTag);
        Task AssignStartupTag(int startupId, int tagId);
        void UnassignStartupTag(int startupId, int tagId);
    }
}
