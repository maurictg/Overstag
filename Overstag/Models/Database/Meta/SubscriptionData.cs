using System.Text.Json.Serialization;
using Overstag.Models.Database.Relations;

namespace Overstag.Models.Database.Meta
{
    public class SubscriptionData
    {
        public int ActivityId { get; set; }
        public int ConsumptionCost { get; set; }
        public int FriendCount { get; set; }

        [JsonIgnore]
        public bool HasFriends { get { return FriendCount > 0; } }

        public SubscriptionData() { }
        public SubscriptionData(Subscription s)
        {
            this.ActivityId = s.ActivityId;
            this.ConsumptionCost = s.ConsumptionCost;
            this.FriendCount = s.FriendCount;
        }
    }
}
