using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public abstract class ProfileResource
    {
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public string MembershipType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public CreditCardResource CreditCard { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        //public List<ProfileTag> ProfileTag { get; set; }
        //public List<Request> Requests { get; set; }
    }
}
