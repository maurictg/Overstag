using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Overstag.Models.Database.Meta;
using System.ComponentModel.DataAnnotations.Schema;

namespace Overstag.Models.Database
{
    [Table("auth")]
    public class Auth
    {
        [Key]
        public string Token { get; set; }
        public AuthType Type { get; set; }

        public Account Account { get; set; }
        public int AccountId { get; set; }

        public DateTime RegisteredAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        //Getters
        public bool IsValid { get { return ExpiresAt < DateTime.Now; } }
    }
}
