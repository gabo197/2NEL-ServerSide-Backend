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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwoNEL.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService creditCardService;
        private readonly IMapper mapper;

        public CreditCardsController(ICreditCardService creditCardService, IMapper mapper)
        {
            this.creditCardService = creditCardService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all Credit Cards",
            Description = "List of Credit Cards",
            OperationId = "ListAllCreditCard")]
        [SwaggerResponse(200, "List of Credit Cards", typeof(IEnumerable<CreditCardResource>))]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CreditCardResource>), 200)]
        public async Task<IEnumerable<CreditCardResource>> GetAllAsync()
        {
            var creditCards = await creditCardService.ListAsync();
            var resources = mapper
                .Map<IEnumerable<CreditCard>, IEnumerable<CreditCardResource>>(creditCards);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CreditCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await creditCardService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var CreditCardResource = mapper.Map<CreditCard, CreditCardResource>(result.Resource);
            return Ok(CreditCardResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreditCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveCreditCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var creditCard = mapper.Map<SaveCreditCardResource, CreditCard>(resource);
            var result = await creditCardService.SaveAsync(userId, creditCard);

            if (!result.Success)
                return BadRequest(result.Message);

            var CreditCardResource = mapper.Map<CreditCard, CreditCardResource>(result.Resource);
            return Ok(CreditCardResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CreditCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCreditCardResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var creditCard = mapper.Map<SaveCreditCardResource, CreditCard>(resource);
            var result = await creditCardService.UpdateAsync(id, creditCard);

            if (!result.Success)
                return BadRequest(result.Message);

            var CreditCardResource = mapper.Map<CreditCard, CreditCardResource>(result.Resource);
            return Ok(CreditCardResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CreditCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await creditCardService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var CreditCardResource = mapper.Map<CreditCard, CreditCardResource>(result.Resource);
            return Ok(CreditCardResource);
        }
    }
}
