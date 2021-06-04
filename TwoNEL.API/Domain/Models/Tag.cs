using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProfileTag> ProfileTags { get; set; }
        public List<StartupTag> StartupTags { get; set; }
    }
}
