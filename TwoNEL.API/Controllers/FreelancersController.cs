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
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly IFreelancerService freelancerService;
        private readonly IMapper mapper;

        public FreelancersController(IFreelancerService freelancerService, IMapper mapper)
        {
            this.freelancerService = freelancerService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all freelancers",
            Description = "List of Freelancers",
            OperationId = "ListAllFreelancers")]
        [SwaggerResponse(200, "List of Freelancers", typeof(IEnumerable<FreelancerResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FreelancerResource>), 200)]
        public async Task<IEnumerable<FreelancerResource>> GetAllAsync()
        {
            var freelancers = await freelancerService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<Freelancer>, IEnumerable<FreelancerResource>>(freelancers);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FreelancerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await freelancerService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var freelancerResource = mapper.Map<Freelancer, FreelancerResource>(result.Resource);
            return Ok(freelancerResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(FreelancerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveFreelancerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var freelancer = mapper.Map<SaveFreelancerResource, Freelancer>(resource);
            var result = await freelancerService.SaveAsync(userId, freelancer);

            if (!result.Success)
                return BadRequest(result.Message);

            var freelancerResource = mapper.Map<Freelancer, FreelancerResource>(result.Resource);
            return Ok(freelancerResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(FreelancerResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFreelancerResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var freelancer = mapper.Map<SaveFreelancerResource, Freelancer>(resource);
            var result = await freelancerService.UpdateAsync(id, freelancer);

            if (!result.Success)
                return BadRequest(result.Message);

            var freelancerResource = mapper.Map<Freelancer, FreelancerResource>(result.Resource);
            return Ok(freelancerResource);
        }
    }
}
