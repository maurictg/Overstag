using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Overstag.Models.Database
{
    /**
     * A group is used to connect users to each other to share the payment on one invoice. Like for a family.
     */
    [Table("user_group")]
    public class Group
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public string Token { get; set; }

        [JsonIgnore]
        public List<User> Participants { get; set; }
    }
}
