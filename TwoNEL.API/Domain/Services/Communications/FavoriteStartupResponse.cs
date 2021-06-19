using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class FavoriteStartupResponse : BaseResponse<FavoriteStartup>
    {
        public FavoriteStartupResponse(FavoriteStartup resource) : base(resource)
        {
        }

        public FavoriteStartupResponse(string message) : base(message)
        {
        }
    }
}
