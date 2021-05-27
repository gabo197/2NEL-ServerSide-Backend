//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Swashbuckle.AspNetCore.Annotations;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TwoNEL.API.Domain.Models;
//using TwoNEL.API.Domain.Services;
//using TwoNEL.API.Extensions;
//using TwoNEL.API.Resources;

//namespace TwoNEL.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProfilesController : ControllerBase
//    {
//        private readonly IEntrepreneurService entrepreneurService;
//        private readonly IFreelancerService freelancerService;
//        private readonly IInvestorService investorService;
//        private readonly IMapper mapper;

//        public ProfilesController(IEntrepreneurService entrepreneurService,
//            IFreelancerService freelancerService, IInvestorService investorService,
//            IMapper mapper)
//        {
//            this.entrepreneurService = entrepreneurService;
//            this.freelancerService = freelancerService;
//            this.investorService = investorService;
//            this.mapper = mapper;
//        }

//        [SwaggerOperation(
//           Summary = "List all profile",
//           Description = "List of Profiles",
//           OperationId = "ListAllProfiles")]
//        [SwaggerResponse(200, "List of Profiles", typeof(IEnumerable<ProfileResource>))]
//        [HttpGet]
//        [ProducesResponseType(typeof(IEnumerable<ProfileResource>), 200)]
//        public async Task<IEnumerable<ProfileResource>> GetAllInvestorsAsync()
//        {
//            var investors = await investorService.ListAsync();
//            var entrepreneurs = await entrepreneurService.ListAsync();
//            var freelancers = await freelancerService.ListAsync();

//            var resources = mapper
//                .Map<IEnumerable<Investor>, IEnumerable<InvestorResource>>(investors);
//            mapper.Map<IEnumerable<Entrepreneur>, IEnumerable<EntrepreneurResource>>(entrepreneurs);
//            mapper.Map<IEnumerable<Freelancer>, IEnumerable<FreelancerResource>>(freelancers);

//            return resources;
//        }

//    }
//}
