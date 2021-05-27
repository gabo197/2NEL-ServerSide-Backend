using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveFreelancerResource : SaveProfileResource
    {
        [Required]
        [MaxLength(20)]
        public string Specialty { get; set; }
    }
}
