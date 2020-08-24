using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Overstag.Models.Database
{
    public class User
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Account Account { get; set; }
        public int AccountId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Residence { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }

        public string MollieToken { get; set; }

        //Getters
        public int Age { get { return new DateTime(DateTime.Now.Year, BirthDate.Month, BirthDate.Day) > DateTime.Now ? (DateTime.Now.Year - BirthDate.Year) - 1 : (DateTime.Now.Year - BirthDate.Year);  } }
    }
}
