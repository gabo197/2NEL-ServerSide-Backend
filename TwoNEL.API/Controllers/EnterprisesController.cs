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
    public class EnterprisesController : ControllerBase
    {
        private readonly IEnterpriseService enterpriseService;
        private readonly IMapper mapper;

        public EnterprisesController(IEnterpriseService enterpriseService, IMapper mapper)
        {
            this.enterpriseService = enterpriseService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all enterprises",
            Description = "List of Enterprises",
            OperationId = "ListAllEnterprises")]
        [SwaggerResponse(200, "List of Enterprises", typeof(IEnumerable<EnterpriseResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnterpriseResource>), 200)]
        public async Task<IEnumerable<EnterpriseResource>> GetAllAsync()
        {
            var enterprises = await enterpriseService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<Enterprise>, IEnumerable<EnterpriseResource>>(enterprises);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EnterpriseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await enterpriseService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var enterpriseResource = mapper.Map<Enterprise, EnterpriseResource>(result.Resource);
            return Ok(enterpriseResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(EnterpriseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveEnterpriseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var enterprise = mapper.Map<SaveEnterpriseResource, Enterprise>(resource);
            var result = await enterpriseService.SaveAsync(userId, enterprise);

            if (!result.Success)
                return BadRequest(result.Message);

            var enterpriseResource = mapper.Map<Enterprise, EnterpriseResource>(result.Resource);
            return Ok(enterpriseResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EnterpriseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEnterpriseResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var enterprise = mapper.Map<SaveEnterpriseResource, Enterprise>(resource);
            var result = await enterpriseService.UpdateAsync(id, enterprise);

            if (!result.Success)
                return BadRequest(result.Message);

            var enterpriseResource = mapper.Map<Enterprise, EnterpriseResource>(result.Resource);
            return Ok(enterpriseResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(EnterpriseResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await enterpriseService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var enterpriseResource = mapper.Map<Enterprise, EnterpriseResource>(result.Resource);
            return Ok(enterpriseResource);
        }
    }
}
