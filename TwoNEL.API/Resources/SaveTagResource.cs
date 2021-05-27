using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveTagResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
