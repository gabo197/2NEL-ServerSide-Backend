using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class InvestorResponse : BaseResponse<Investor>
    {
        public InvestorResponse(Investor resource) : base(resource)
        {
        }

        public InvestorResponse(string message) : base(message)
        {
        }
    }
}
