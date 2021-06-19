using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public abstract class Profile
    {
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
        public User User { get; set; }
        public EMembershipType MembershipType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Portfolio { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public List<ProfileTag> ProfileTags { get; set; }
        public ICollection<Request> Requests { get; set; }
        public List<FavoriteProfile> FavoriteProfiles { get; set; }
        public List<FavoriteStartup> FavoriteStartups { get; set; }
    }
}
