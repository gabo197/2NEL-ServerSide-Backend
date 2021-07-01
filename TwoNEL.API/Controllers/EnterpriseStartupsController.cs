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
    [Route("api/enterprises/{enterpriseId}/startups")]
    [ApiController]
    public class EnterpriseStartupsController : ControllerBase
    {
        private readonly IStartupService startupService;
        private readonly IMapper mapper;

        public EnterpriseStartupsController(IStartupService startupService, IMapper mapper)
        {
            this.startupService = startupService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all startups by enterpriseId",
            Description = "List of Startups",
            OperationId = "ListAllStartups")]
        [SwaggerResponse(200, "List of Startups", typeof(IEnumerable<StartupResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StartupResource>), 200)]
        public async Task<IEnumerable<StartupResource>> GetAllByEnterpriseIdAsync(int enterpriseId)
        {
            var startups = await startupService.ListByEnterpriseIdAsync(enterpriseId);
            var resources = mapper
                .Map<IEnumerable<Domain.Models.Startup>, IEnumerable<StartupResource>>(startups);
            return resources;
        }

        [HttpGet("{startupId}")]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int enterpriseId, int startupId)
        {
            var result = await startupService.GetByIdAsync(enterpriseId, startupId);
            if (!result.Success)
                return BadRequest(result.Message);
            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(startupResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int enterpriseId, [FromBody] SaveStartupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var startup = mapper.Map<SaveStartupResource, Domain.Models.Startup>(resource);
            var result = await startupService.SaveAsync(enterpriseId, startup);

            if (!result.Success)
                return BadRequest(result.Message);

            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(startupResource);
        }

        [HttpPut("{startupId}")]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int enterpriseId, int startupId, [FromBody] SaveStartupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var startup = mapper.Map<SaveStartupResource, Domain.Models.Startup>(resource);
            var result = await startupService.UpdateAsync(enterpriseId, startupId, startup);

            if (!result.Success)
                return BadRequest(result.Message);

            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(startupResource);
        }

        [HttpDelete("{startupId}")]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int enterpriseId, int startupId)
        {
            var result = await startupService.DeleteAsync(enterpriseId, startupId);

            if (!result.Success)
                return BadRequest(result.Message);

            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(startupResource);
        }
    }
}
