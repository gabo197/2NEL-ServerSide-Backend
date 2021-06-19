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

namespace TwoNEL.API.Controllers
{
    [Route("api/profile/{userId}/tags")]
    [ApiController]
    public class ProfileTagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IProfileTagService _productTagService;
        private readonly IMapper _mapper;

        public ProfileTagsController(ITagService tagService, IProfileTagService productTagService, IMapper mapper)
        {
            _tagService = tagService;
            _productTagService = productTagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TagResource>> GetAllByUserIdAsync(int userId)
        {
            var tags = await _tagService.ListByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Tag>, IEnumerable<TagResource>>(tags);
            return resources;
        }

        [HttpPost("{tagId}")]
        public async Task<IActionResult> AssignProfileTag(int userId, int tagId)
        {
            var result = await _productTagService.AssignProfileTagAsync(userId, tagId);
            if (!result.Success)
                return BadRequest(result.Message);

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource.Tag);
            return Ok(tagResource);
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> UnassignProfileTag(int userId, int tagId)
        {
            var result = await _productTagService.UnassignProfileTagAsync(userId, tagId);
            if (!result.Success)
                return BadRequest(result.Message);

            var tagResource = _mapper.Map<Tag, TagResource>(result.Resource.Tag);
            return Ok(tagResource);
        }
    }
}
