using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;
using TwoNEL.API.Extensions;
using TwoNEL.API.Resources;

namespace TwoNEL.API.Mapping
{
    public class ModelToResourceProfile : AutoMapper.Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<CreditCard, CreditCardResource>();

            CreateMap<User, UserResource>();

            //CreateMap<Domain.Models.Profile, ProfileResource>()
            //    .ForMember(src => src.MembershipType,
            //    opt => opt.MapFrom(src => src.MembershipType.ToDescriptionString()));

            CreateMap<Entrepreneur, EntrepreneurResource>();


            CreateMap<Investor, InvestorResource>();

            CreateMap<Freelancer, FreelancerResource>();

            CreateMap<Tag, TagResource>();

            CreateMap<Enterprise, EnterpriseResource>();

            CreateMap<Request, RequestResource>();

            CreateMap<Domain.Models.Startup, StartupResource>();
        }
    }
}
