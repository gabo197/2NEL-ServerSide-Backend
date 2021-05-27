using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class TagResponse : BaseResponse<Tag>
    {
        public TagResponse(Tag resource) : base(resource)
        {
        }

        public TagResponse(string message) : base(message)
        {
        }
    }
}
