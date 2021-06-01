using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveRequestResource
    {
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        public int RecieverId { get; set; }
    }
}
