using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class UserTagResponse : BaseResponse<ProfileTag>
    {
        public UserTagResponse(ProfileTag resource) : base(resource)
        {
        }

        public UserTagResponse(string message) : base(message)
        {
        }
    }
}
