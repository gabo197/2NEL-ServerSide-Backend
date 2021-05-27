using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class EntrepreneurResponse : BaseResponse<Entrepreneur>
    {
        public EntrepreneurResponse(Entrepreneur resource) : base(resource)
        {
        }

        public EntrepreneurResponse(string message) : base(message)
        {
        }
    }
}
