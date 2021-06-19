using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class FavoriteProfile
    {
        public int UserId { get; set; }
        public Profile Profile { get; set; }
        public int FavoriteId { get; set; }
        public Profile Favorite { get; set; }
    }
}
