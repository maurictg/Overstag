using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Overstag.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentID { get; set; }
        public Mollie.Api.Models.Payment.PaymentStatus? Status { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? PayedAt { get; set; }

        //Relations
        public Account User { get; set; }

        [JsonIgnore] //Ignore inovice to prevent object cycle
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
