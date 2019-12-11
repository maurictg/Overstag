using System.Collections.Generic;
using System;
using Overstag.Models;
using System.ComponentModel.DataAnnotations;

namespace Overstag.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int Sex { get; set; }
        //0 (male) /1 (female)
        public int Type { get; set; }
        //0 user, 1 parent, 2 mentor, 3 admin
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Token { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Residence { get; set; }
        public DateTime Birthdate { get; set; }

        public string MollieID { get; set; }

        public string TwoFactor { get; set; }
        public string TwoFactorCodes { get; set; }
        
        //Policies

        //Relations
        public Family Family { get; set; }
        public List<Participate> Subscriptions { get; set; }
        public List<Vote> Votes { get; set; }
    }

    public class Logging
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public string Ip { get; set; }
    }
}
