﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Overstag.Models.Database.Relations
{
    //User - Activity
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
        public bool HasFriends { get { return FriendCount > 0; } }
    }
}
