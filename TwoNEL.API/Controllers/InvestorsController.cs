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
    public class InvestorsController : ControllerBase
    {
        private readonly IInvestorService investorService;
        private readonly IMapper mapper;

        public InvestorsController(IInvestorService investorService, IMapper mapper)
        {
            this.investorService = investorService;
            this.mapper = mapper;
        }


        [SwaggerOperation(
            Summary = "List all investors",
            Description = "List of Investors",
            OperationId = "ListAllInvestors")]
        [SwaggerResponse(200, "List of Investors", typeof(IEnumerable<InvestorResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvestorResource>), 200)]
        public async Task<IEnumerable<InvestorResource>> GetAllAsync()
        {
            var investors = await investorService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<Investor>, IEnumerable<InvestorResource>>(investors);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(InvestorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await investorService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var investorResource = mapper.Map<Investor, InvestorResource>(result.Resource);
            return Ok(investorResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(InvestorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveInvestorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var investor = mapper.Map<SaveInvestorResource, Investor>(resource);
            var result = await investorService.SaveAsync(userId, investor);

            if (!result.Success)
                return BadRequest(result.Message);

            var investorResource = mapper.Map<Investor, InvestorResource>(result.Resource);
            return Ok(investorResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(InvestorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveInvestorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var investor = mapper.Map<SaveInvestorResource, Investor>(resource);
            var result = await investorService.UpdateAsync(id, investor);

            if (!result.Success)
                return BadRequest(result.Message);

            var investorResource = mapper.Map<Investor, InvestorResource>(result.Resource);
            return Ok(investorResource);
        }
    }
}
