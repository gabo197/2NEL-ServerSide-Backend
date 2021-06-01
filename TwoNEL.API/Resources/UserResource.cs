using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        //public int ProfileId { get; set; }
        //public ProfileResource Profile { get; set; }
        //public EntrepreneurResource EProfile { get; set; }
        //public FreelancerResource FProfile { get; set; }
        //public InvestorResource IProfile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public int CreditCardId { get; set; }
        // public CreditCardResource CreditCard { get; set; }
        public DateTime RegisterDate { get; set; }

    }
}
