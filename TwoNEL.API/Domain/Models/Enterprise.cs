using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Enterprise
    {
        // public int Id { get; set; }
        public int EntrepreneurId { get; set; }
        public Entrepreneur Entrepreneur { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BusinessEmail { get; set; }
        public string CorpNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public IList<Models.Startup> StartUps { get; set; } = new List<Models.Startup>();
        public IList<Request> Requests { get; set; } = new List<Request>();

    }
}
