using AutoMapper;
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
    [Route("api/profile/{userId}/favoriteProfiles")]
    [ApiController]
    public class FavoriteProfilesController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IFavoriteProfileService favoriteProfileService;
        private readonly IMapper mapper;

        public FavoriteProfilesController(IUserService userService, IFavoriteProfileService favoriteProfileService, IMapper mapper)
        {
            this.userService = userService;
            this.favoriteProfileService = favoriteProfileService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProfileResource>> GetAllByUserIdAsync(int userId)
        {
            var favorites = await userService.ListByUserIdAsync(userId);
            var resources = mapper.Map<IEnumerable<Domain.Models.Profile>, IEnumerable<ProfileResource>>(favorites);
            return resources;
        }

        [HttpPost("{favoriteId}")]
        public async Task<IActionResult> AssignFavoriteStartup(int userId, int favoriteId)
        {
            var result = await favoriteProfileService.AssignFavoriteProfileAsync(userId, favoriteId);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource.Profile);
            return Ok(profileResource);
        }

        [HttpDelete("{favoriteId}")]
        public async Task<IActionResult> UnassignProfileTag(int userId, int favoriteId)
        {
            var result = await favoriteProfileService.UnassignFavoriteProfileAsync(userId, favoriteId);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource.Profile);
            return Ok(profileResource);
        }
    }
}
