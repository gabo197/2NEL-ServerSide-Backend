using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class RequestResponse : BaseResponse<Request>
    {
        public RequestResponse(Request resource) : base(resource)
        {
        }

        public RequestResponse(string message) : base(message)
        {
        }
    }
}
