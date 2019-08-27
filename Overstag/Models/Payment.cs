using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string PaymentID { get; set; }
        public string InvoiceID { get; set; }
        public int UserID { get; set; }
        public Mollie.Api.Models.Payment.PaymentStatus? Status { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? PayedAt { get; set; }
    }
}
