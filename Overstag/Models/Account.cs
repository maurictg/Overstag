using System.Collections.Generic;
using Overstag.Models;

namespace Overstag.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }

        public List<Participate> Participants { get; set; } = new List<Participate>();
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
