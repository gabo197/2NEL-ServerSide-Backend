using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoNEL.API.Domain.Models;

namespace TwoNEL.API.Domain.Services.Communications
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(User user, string token)
        {
            Id = user.Id;
            Email = user.Email;
            Token = token;
        }

        public AuthenticationResponse()
        {
        }
    }
}
