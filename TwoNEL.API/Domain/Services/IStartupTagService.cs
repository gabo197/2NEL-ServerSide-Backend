using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services.Communications;

namespace TwoNEL.API.Domain.Services
{
    public interface IStartupTagService
    {
        Task<IEnumerable<StartupTag>> ListAsync();
        Task<IEnumerable<StartupTag>> ListByStartupIdAsync(int startupId);
        Task<IEnumerable<StartupTag>> ListByTagIdAsync(int tagId);
        Task<StartupTagResponse> AssignStartupTagAsync(int startupId, int tagId);
        Task<StartupTagResponse> UnassignStartupTagAsync(int startupId, int tagId);
    }
}
