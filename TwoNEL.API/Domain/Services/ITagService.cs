using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> ListAsync();
        Task<IEnumerable<Tag>> ListByStartupIdAsync(int startupId);
        Task<IEnumerable<Tag>> ListByUserIdAsync(int userId);
        Task<TagResponse> GetByIdAsync(int id);
        Task<TagResponse> SaveAsync(Tag tag);
        Task<TagResponse> UpdateAsync(int id, Tag tag);
        Task<TagResponse> DeleteAsync(int id);
    }
}
