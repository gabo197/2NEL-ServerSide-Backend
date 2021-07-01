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
    public class EntrepreneursController : ControllerBase
    {
        private readonly IEntrepreneurService entrepreneurService;
        private readonly IMapper mapper;

        public EntrepreneursController(IEntrepreneurService entrepreneurService, IMapper mapper)
        {
            this.entrepreneurService = entrepreneurService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all entrepreneurs",
            Description = "List of Entrepreneurs",
            OperationId = "ListAllEntrepreneurs")]
        [SwaggerResponse(200, "List of Entrepreneurs", typeof(IEnumerable<EntrepreneurResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EntrepreneurResource>), 200)]
        public async Task<IEnumerable<EntrepreneurResource>> GetAllAsync()
        {
            var entrepreneurs = await entrepreneurService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<Entrepreneur>, IEnumerable<EntrepreneurResource>>(entrepreneurs);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EntrepreneurResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await entrepreneurService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var entrepreneurResource = mapper.Map<Entrepreneur, EntrepreneurResource>(result.Resource);
            return Ok(entrepreneurResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EntrepreneurResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveEntrepreneurResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var entrepreneur = mapper.Map<SaveEntrepreneurResource, Entrepreneur>(resource);
            var result = await entrepreneurService.SaveAsync(userId, entrepreneur);

            if (!result.Success)
                return BadRequest(result.Message);

            var entrepreneurResource = mapper.Map<Entrepreneur, EntrepreneurResource>(result.Resource);
            return Ok(entrepreneurResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EntrepreneurResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEntrepreneurResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var entrepreneur = mapper.Map<SaveEntrepreneurResource, Entrepreneur>(resource);
            var result = await entrepreneurService.UpdateAsync(id, entrepreneur);

            if (!result.Success)
                return BadRequest(result.Message);

            var entrepreneurResource = mapper.Map<Entrepreneur, EntrepreneurResource>(result.Resource);
            return Ok(entrepreneurResource);
        }
    }
}
