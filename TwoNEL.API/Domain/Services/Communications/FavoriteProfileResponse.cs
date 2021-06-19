using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class FavoriteProfileResponse : BaseResponse<FavoriteProfile>
    {
        public FavoriteProfileResponse(FavoriteProfile resource) : base(resource)
        {
        }

        public FavoriteProfileResponse(string message) : base(message)
        {
        }
    }
}
