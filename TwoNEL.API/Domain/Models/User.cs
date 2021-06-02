using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwoNEL.API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public Profile Profile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public CreditCard CreditCard { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
