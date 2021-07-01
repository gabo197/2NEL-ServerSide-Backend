using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Domain.Services;
using TwoNEL.API.Extensions;
using TwoNEL.API.Resources;

namespace TwoNEL.API.Controllers
{
    [Authorize]
    [Route("api/profile/{userId}/favoriteStartups")]
    [ApiController]
    public class FavoriteStartupsController : ControllerBase
    {
        private readonly IStartupService startupService;
        private readonly IUserService userService;
        private readonly IFavoriteStartupService favoriteStartupService;
        private readonly IMapper mapper;

        public FavoriteStartupsController(IStartupService startupService, IUserService userService, IFavoriteStartupService favoriteStartupService, IMapper mapper)
        {
            this.startupService = startupService;
            this.userService = userService;
            this.favoriteStartupService = favoriteStartupService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<StartupResource>> GetAllByUserIdAsync(int userId)
        {
            var favorites = await startupService.ListByUserIdAsync(userId);
            var resources = mapper.Map<IEnumerable<Domain.Models.Startup>, IEnumerable<StartupResource>>(favorites);
            return resources;
        }

        [HttpPost("{startupId}")]
        public async Task<IActionResult> AssignFavoriteStartup(int userId, int startupId)
        {
            var result = await favoriteStartupService.AssignFavoriteStartupAsync(userId, startupId);
            if (!result.Success)
                return BadRequest(result.Message);

            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource.Startup);
            return Ok(startupResource);
        }

        [HttpDelete("{startupId}")]
        public async Task<IActionResult> UnassignFavoriteStartup(int userId, int startupId)
        {
            var result = await favoriteStartupService.UnassignFavoriteStartupAsync(userId, startupId);
            if (!result.Success)
                return BadRequest(result.Message);

            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource.Startup);
            return Ok(startupResource);
        }
    }
}
