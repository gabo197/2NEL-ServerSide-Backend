using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class EnterpriseResource
    {
        // public int Id { get; set; }
        public int EntrepreneurId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessEmail { get; set; }
        public string CorpNumber { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
