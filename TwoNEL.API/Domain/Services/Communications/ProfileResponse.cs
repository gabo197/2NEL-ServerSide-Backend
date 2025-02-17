﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class ProfileResponse : BaseResponse<Profile>
    {
        public ProfileResponse(Profile resource) : base(resource)
        {
        }

        public ProfileResponse(string message) : base(message)
        {
        }
    }
}
