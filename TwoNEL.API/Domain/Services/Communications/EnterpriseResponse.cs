using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class EnterpriseResponse : BaseResponse<Enterprise>
    {
        public EnterpriseResponse(Enterprise resource) : base(resource)
        {
        }

        public EnterpriseResponse(string message) : base(message)
        {
        }
    }
}