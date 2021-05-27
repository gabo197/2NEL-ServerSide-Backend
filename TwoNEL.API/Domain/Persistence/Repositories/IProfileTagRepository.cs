using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Persistence.Repositories
{
    public interface IProfileTagRepository
    {
        Task<IEnumerable<ProfileTag>> ListAsync();
        Task<IEnumerable<ProfileTag>> ListByUserIdAsync(int userId);
        Task<IEnumerable<ProfileTag>> ListByTagIdAsync(int tagId);
        Task<ProfileTag> FindByUserIdAndTagId(int userId, int tagId);
        Task AddAsync(ProfileTag userTag);
        void Remove(ProfileTag userTag);
        Task AssignUserTag(int userId, int tagId);
        void UnassignUserTag(int userId, int tagId);
    }
}
