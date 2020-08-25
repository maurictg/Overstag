using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    [Table("invoice")]
    public class Invoice
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int Amount { get; set; } //Amount in cents
        public int Number { get; set; } //Invoice number
        public DateTime Timestamp { get; set; }

        public UserData UserData { get; set; } //JSON
        public List<SubscriptionData> Subscriptions { get; set; } //JSON

        [JsonIgnore]
        public User User { get; set; }
        public int? UserId { get; set; }

        [JsonIgnore]
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }

        /// <summary>
        /// Indicates if an invoice is paid. Make sure that the payment is included, else it will always return false.
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public bool Paid { get { return Payment != null && Payment.Paid; } }
    }
}
