using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.API
{
    public class SubscriptionInfo
    {
        public ActivityInfo? Activity { get; set; }
        public int EventID { get; set; }
        public bool Factured { get; set; }
        public double DrinksCost { get; set; }
        public int FriendCount { get; set; }
    }
}
