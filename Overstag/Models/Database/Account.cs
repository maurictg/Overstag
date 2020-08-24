using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    public class Account
    {
        public int Id { get; set; }
        public AccountType Type { get; set; }

        public User User { get; set; }
        public int? UserId { get; set; }

        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Secret { get; set; }
        public string BackupCodes { get; set; }
    }
}
