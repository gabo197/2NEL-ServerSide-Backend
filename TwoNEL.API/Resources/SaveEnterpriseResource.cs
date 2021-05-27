using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveEnterpriseResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [EmailAddress]
        public string BusinessEmail { get; set; }

        [Required]
        [MaxLength(9)]
        [RegularExpression("^[0-9]*$")]
        public string CorpNumber { get; set; }
    }
}
