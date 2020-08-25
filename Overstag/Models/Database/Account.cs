using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    [Table("account")]
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

        [JsonIgnore]
        public List<Auth> Auths { get; set; }
    }
}
