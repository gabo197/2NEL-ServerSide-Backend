using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IProfileTagService
    {
        Task<IEnumerable<ProfileTag>> ListAsync();
        Task<IEnumerable<ProfileTag>> ListByUserIdAsync(int userId);
        Task<IEnumerable<ProfileTag>> ListByTagIdAsync(int tagId);
        Task<ProfileTagResponse> AssignProfileTagAsync(int userId, int tagId);
        Task<ProfileTagResponse> UnassignProfileTagAsync(int userId, int tagId);
    }
}
