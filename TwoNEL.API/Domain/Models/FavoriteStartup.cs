using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class FavoriteStartup
    {
        public int UserId { get; set; }
        public Profile Profile { get; set; }
        public int StartupId { get; set; }
        public Startup Startup { get; set; }
    }
}
