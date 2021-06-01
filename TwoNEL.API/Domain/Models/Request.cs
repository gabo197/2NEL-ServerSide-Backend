using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public int SenderId { get; set; }
        public Profile Sender { get; set; }
        public int ReceiverId { get; set; }
        public Profile Receiver { get; set; }
    }
}
