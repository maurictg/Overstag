using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Mollie.Api.Models.Payment;

namespace Overstag.Models
{
    public enum PayType { MOLLIE = 0, BANK = 1, CRYPTO = 2 };

    public class Payment
    {
        public int Id { get; set; }
        public string PaymentId { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public PayType PayType { get; set; }
        public PaymentStatus? Status { get; set; }
        public string Metadata { get; set; }

        public bool IsPaid() => (Status == PaymentStatus.Paid);
        public bool IsFailed() => (Status == PaymentStatus.Canceled || Status == PaymentStatus.Expired || Status == PaymentStatus.Failed);

        //Relations
        [JsonIgnore]
        public Account User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore] //Ignore inovice to prevent object cycle
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
