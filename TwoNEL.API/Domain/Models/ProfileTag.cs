using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class ProfileTag
    {
        public int UserId { get; set; }
        public Profile Profile { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
