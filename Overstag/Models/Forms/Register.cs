using Overstag.Encryption;
using Overstag.Models.Database;
using Overstag.Models.Database.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.Forms
{
    //Register form used in Register.cshtml and RegisterController
    public class Register
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Postalcode { get; set; }
        public string Residence { get; set; }
        public string Username { get; set; }
        public DateTime Birthdate { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public int Type { get; set; }

        public User GetUser()
        {
            //Make name case sensitive
            char[] l = Lastname.ToCharArray();
            int i = (Lastname.Contains(' ')) ? Lastname.TrimEnd(' ').LastIndexOf(' ') + 1 : 0;
            l[i] = char.ToUpper(l[i]);

            return new User()
            {
                FirstName = Firstname[0].ToString().ToUpper() + Firstname.Substring(1),
                LastName = new string(l),
                Address = Address,
                PostalCode = Postalcode,
                Residence = Residence,
                BirthDate = Birthdate,
                Gender = Gender,
                Phone = Phone
            };
        }

        public Account GetAccount() => new Account()
        {
            Email = Email,
            Type = (Username == "admin") ? AccountType.ADMIN : (Type == 1) ? AccountType.PARENT : AccountType.USER,
            Username = Username,
            Password = PBKDF2.Hash(Password)
        };
    }
}
