using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Overstag.Models
{
    public enum PayType { MOLLIE, BANK, CRYPTO, NONE };

    public class Payment
    {
        public int Id { get; set; }
        public string PaymentId { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public PayType PayType { get; set; }
        public Mollie.Api.Models.Payment.PaymentStatus? Status { get; set; }
        public string Metadata { get; set; }

        public bool IsPayed() => (Status == Mollie.Api.Models.Payment.PaymentStatus.Paid);

        //Relations
        [JsonIgnore]
        public Account User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore] //Ignore inovice to prevent object cycle
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
