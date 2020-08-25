using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Overstag.Models.Database.Relations
{
    //User - Activity
    [Table("user_activity")]
    public class Subscription
    {
        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public Activity Activity { get; set; }
        public int ActivityId { get; set; }

        public bool Billed { get; set; }
        public int ConsumptionCost { get; set; }
        public int FriendCount { get; set; }

        //Getters
        [NotMapped]
        [JsonIgnore]
        public bool HasFriends { get { return FriendCount > 0; } }
    }
}
