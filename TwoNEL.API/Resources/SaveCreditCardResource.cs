using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class SaveCreditCardResource
    {
        [Required]
        //[CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(4)]
        [RegularExpression("^[0-9]*$")]
        public string Cvv { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression("^[0-9]*$")]
        public string ExpMonth { get; set; }

        [Required]
        [StringLength(2)]
        [RegularExpression("^[0-9]*$")]
        public string ExpYear { get; set; }
    }
}
