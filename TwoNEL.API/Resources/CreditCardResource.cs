using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class CreditCardResource
    {
        public int UserId { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }
    }
}
