using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class StartupTag
    {
        public int StartupId { get; set; }
        public Startup Startup { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
