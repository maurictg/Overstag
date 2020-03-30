using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.API
{
    public class UserInfo
    {
        public bool Gender { get; set; }
        //1 (male) /0 (female)
        public string Username { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Postalcode { get; set; }
        public string Residence { get; set; }
        public DateTime Birthdate { get; set; }
    }
}
