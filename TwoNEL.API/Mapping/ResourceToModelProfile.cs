using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Resources;

namespace TwoNEL.API.Mapping
{
    public class ResourceToModelProfile : AutoMapper.Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCreditCardResource, CreditCard>();

            CreateMap<SaveEnterpriseResource, Enterprise>();

            CreateMap<SaveRequestResource, Request>();

            CreateMap<SaveTagResource, Tag>();

            CreateMap<SaveStartupResource, Domain.Models.Startup>();

            CreateMap<SaveUserResource, User>();

            CreateMap<SaveProfileResource, Domain.Models.Profile>();

            CreateMap<SaveEntrepreneurResource, Entrepreneur>();

            CreateMap<SaveFreelancerResource, Freelancer>();

            CreateMap<SaveInvestorResource, Investor>();
        }
    }
}
