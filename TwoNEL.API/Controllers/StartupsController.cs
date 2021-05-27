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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoNEL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StartupsController : ControllerBase
    {
        private readonly IStartupService startupService;
        private readonly IMapper mapper;

        public StartupsController(IStartupService startupService, IMapper mapper)
        {
            this.startupService = startupService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all startups",
            Description = "List of Startups",
            OperationId = "ListAllStartups")]
        [SwaggerResponse(200, "List of Startups", typeof(IEnumerable<StartupResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StartupResource>), 200)]
        public async Task<IEnumerable<StartupResource>> GetAllAsync()
        {
            var startups = await startupService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<Domain.Models.Startup>, IEnumerable<StartupResource>>(startups);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await startupService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(startupResource);
        }

        //[HttpPost]
        //[ProducesResponseType(typeof(StartupResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> PostAsync([FromBody] SaveStartupResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var category = mapper.Map<SaveStartupResource, Domain.Models.Startup>(resource);
        //    var result = await startupService.SaveAsync(category);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var startupResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
        //    return Ok(startupResource);
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(typeof(StartupResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> PutAsync(int id, [FromBody] SaveStartupResource resource)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrorMessages());

        //    var category = mapper.Map<SaveStartupResource, Domain.Models.Startup>(resource);
        //    var result = await startupService.UpdateAsync(id, category);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    var categoryResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
        //    return Ok(categoryResource);
        //}

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(StartupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await startupService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var categoryResource = mapper.Map<Domain.Models.Startup, StartupResource>(result.Resource);
            return Ok(categoryResource);
        }
    }
}
