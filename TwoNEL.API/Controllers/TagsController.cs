using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TagResource>> GetAllAsync()
        {
            var tags = await _tagService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveTagResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var tag = _mapper.Map<SaveTagResource, Tag>(resource);
            var result = await _tagService.SaveAsync(tag);

            if (!result.Success)
                return BadRequest(result.Message);

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource);
            return Ok(tagResource);
        }
    }
}
