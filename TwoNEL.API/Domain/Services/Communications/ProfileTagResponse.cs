using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class ProfileTagResponse : BaseResponse<ProfileTag>
    {
        public ProfileTagResponse(ProfileTag resource) : base(resource)
        {
        }

        public ProfileTagResponse(string message) : base(message)
        {
        }
    }
}
