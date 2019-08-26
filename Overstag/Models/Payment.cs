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
        DateTime Timestamp { get; set; }
    }
}
