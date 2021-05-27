using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Freelancer : Profile
    {
        public string Specialty { get; set; }
        //public List<Models.Startup> BookMarkedStartups { get; set; }
    }
}
