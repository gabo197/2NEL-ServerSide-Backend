using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Resources
{
    public class RequestResource
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int SenderId { get; set; }
        public int RecieverId { get; set; }
        public DateTime Date { get; set; }
    }
}
