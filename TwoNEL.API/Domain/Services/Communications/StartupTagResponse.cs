using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class StartupTagResponse : BaseResponse<StartupTag>
    {
        public StartupTagResponse(StartupTag resource) : base(resource)
        {
        }

        public StartupTagResponse(string message) : base(message)
        {
        }
    }
}
