using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Profile Profile { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
    }
}
