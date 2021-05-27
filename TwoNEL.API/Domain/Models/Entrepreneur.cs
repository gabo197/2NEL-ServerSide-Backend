using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Entrepreneur : Profile
    {
        public int EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
        //public List<Investor> BookMarkedInvestors { get; set; }
        //public List<Freelancer> BookMarkedFreelancers { get; set; }
    }
}
