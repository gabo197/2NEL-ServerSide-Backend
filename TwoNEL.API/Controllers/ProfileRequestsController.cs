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
    [Route("api/profile/{userId}/requests")]
    [ApiController]
    public class ProfileRequestsController : ControllerBase
    {
        private readonly IRequestService requestService;
        private readonly IMapper mapper;

        public ProfileRequestsController(IRequestService requestService, IMapper mapper)
        {
            this.requestService = requestService;
            this.mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "List all requests by userId",
            Description = "List of Requests",
            OperationId = "ListAllRequests")]
        [SwaggerResponse(200, "List of Requests", typeof(IEnumerable<RequestResource>))]
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RequestResource>), 200)]
        public async Task<IEnumerable<RequestResource>> GetAllByUserIdAsync(int userId)
        {
            var requests = await requestService.ListByUserIdAsync(userId);
            var resources = mapper
                .Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);
            return resources;
        }

        [HttpGet("{requestId}")]
        [ProducesResponseType(typeof(RequestResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int userId, int requestId)
        {
            var result = await requestService.GetByIdAsync(userId, requestId);
            if (!result.Success)
                return BadRequest(result.Message);
            var requestResource = mapper.Map<Domain.Models.Request, RequestResource>(result.Resource);
            return Ok(requestResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RequestResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int userId, [FromBody] SaveRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var request = mapper.Map<SaveRequestResource, Domain.Models.Request>(resource);
            var result = await requestService.SaveAsync(userId, request);

            if (!result.Success)
                return BadRequest(result.Message);

            var requestResource = mapper.Map<Domain.Models.Request, RequestResource>(result.Resource);
            return Ok(requestResource);
        }

        [HttpPut("{requestId}")]
        [ProducesResponseType(typeof(RequestResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int userId, int requestId, [FromBody] SaveRequestResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var request = mapper.Map<SaveRequestResource, Domain.Models.Request>(resource);
            var result = await requestService.UpdateAsync(userId, requestId, request);

            if (!result.Success)
                return BadRequest(result.Message);

            var requestResource = mapper.Map<Domain.Models.Request, RequestResource>(result.Resource);
            return Ok(requestResource);
        }

        [HttpDelete("{requestId}")]
        [ProducesResponseType(typeof(RequestResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId, int requestId)
        {
            var result = await requestService.DeleteAsync(userId, requestId);

            if (!result.Success)
                return BadRequest(result.Message);

            var requestResource = mapper.Map<Domain.Models.Request, RequestResource>(result.Resource);
            return Ok(requestResource);
        }
    }
}
