//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TwoNEL.API.Domain.Models;
//using TwoNEL.API.Domain.Services;
//using TwoNEL.API.Extensions;
//using TwoNEL.API.Resources;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace TwoNEL.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RequestsController : ControllerBase
//    {
//        private readonly IRequestService requestService;
//        private readonly IMapper mapper;

//        public RequestsController(IRequestService requestService, IMapper mapper)
//        {
//            this.requestService = requestService;
//            this.mapper = mapper;
//        }

//        [SwaggerOperation(
//            Summary = "List all requests",
//            Description = "List of Requests",
//            OperationId = "ListAllRequests")]
//        [SwaggerResponse(200, "List of Requests", typeof(IEnumerable<RequestResource>))]
//        [HttpGet]
//        [ProducesResponseType(typeof(IEnumerable<RequestResource>), 200)]
//        public async Task<IEnumerable<RequestResource>> GetAllAsync()
//        {
//            var requests = await requestService.ListAsync();
//            var resources = mapper
//                .Map<IEnumerable<Request>, IEnumerable<RequestResource>>(requests);
//            return resources;
//        }

//        [HttpGet("{id}")]
//        [ProducesResponseType(typeof(RequestResource), 200)]
//        [ProducesResponseType(typeof(BadRequestResult), 404)]
//        public async Task<IActionResult> GetAsync(int id)
//        {
//            var result = await requestService.GetByIdAsync(id);
//            if (!result.Success)
//                return BadRequest(result.Message);
//            var requestResource = mapper.Map<Request, RequestResource>(result.Resource);
//            return Ok(requestResource);
//        }

//        [HttpPost]
//        [ProducesResponseType(typeof(RequestResource), 200)]
//        [ProducesResponseType(typeof(BadRequestResult), 404)]
//        public async Task<IActionResult> PostAsync([FromBody] SaveRequestResource resource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState.GetErrorMessages());

//            var category = mapper.Map<SaveRequestResource, Request>(resource);
//            var result = await requestService.SaveAsync(category);

//            if (!result.Success)
//                return BadRequest(result.Message);

//            var requestResource = mapper.Map<Request, RequestResource>(result.Resource);
//            return Ok(requestResource);
//        }

//        [HttpPut("{id}")]
//        [ProducesResponseType(typeof(RequestResource), 200)]
//        [ProducesResponseType(typeof(BadRequestResult), 404)]
//        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRequestResource resource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState.GetErrorMessages());

//            var category = mapper.Map<SaveRequestResource, Request>(resource);
//            var result = await requestService.UpdateAsync(id, category);

//            if (!result.Success)
//                return BadRequest(result.Message);

//            var categoryResource = mapper.Map<Request, RequestResource>(result.Resource);
//            return Ok(categoryResource);
//        }

//        [HttpDelete("{id}")]
//        [ProducesResponseType(typeof(RequestResource), 200)]
//        [ProducesResponseType(typeof(BadRequestResult), 404)]
//        public async Task<IActionResult> DeleteAsync(int id)
//        {
//            var result = await requestService.DeleteAsync(id);

//            if (!result.Success)
//                return BadRequest(result.Message);

//            var categoryResource = mapper.Map<Request, RequestResource>(result.Resource);
//            return Ok(categoryResource);
//        }
//    }
//}
