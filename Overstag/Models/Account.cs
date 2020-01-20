using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Overstag.Models
{
    public class Account
    {
        public int Id { get; set; }
        public bool Gender { get; set; }
        //1 (male) /0 (female)
        public byte Type { get; set; }
        //0 user, 1 parent, 2 mentor, 3 admin
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }
        
        public string Token { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Residence { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime RegisterDate { get; set; }

        public string MollieID { get; set; }

        public string TwoFactor { get; set; }
        public string TwoFactorCodes { get; set; }
        
        //Relations (Many to one)
        public Family Family { get; set; }
        //Relations (Many to many)
        public List<Participate> Subscriptions { get; set; }
        public List<Vote> Votes { get; set; }
        //Relations (One to many)
        public List<Invoice> Invoices { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Accountancy.Transaction> Transactions { get; set; }
        public List<Auth> Auths { get; set; }
    }
}
