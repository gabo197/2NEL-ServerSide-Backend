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
        Task AddAsync(ProfileTag profileTag);
        void Remove(ProfileTag profileTag);
        Task AssignProfileTag(int userId, int tagId);
        void UnassignProfileTag(int userId, int tagId);
    }
}
