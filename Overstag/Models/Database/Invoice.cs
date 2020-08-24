﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int Amount { get; set; } //Amount in cents
        public int Number { get; set; } //Invoice number
        public DateTime Timestamp { get; set; }

        public string UserData { get; set; } //JSON
        public string SubscriptionData { get; set; } //JSON

        [JsonIgnore]
        public User User { get; set; }
        public int? UserId { get; set; }

        [JsonIgnore]
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        //Getters (userdata, eventdata)
    }
}
