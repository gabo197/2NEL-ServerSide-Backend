using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class CreditCardResponse : BaseResponse<CreditCard>
    {
        public CreditCardResponse(CreditCard resource) : base(resource)
        {
        }

        public CreditCardResponse(string message) : base(message)
        {
        }
    }
}
