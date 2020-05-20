using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.API
{
    public class InvoiceInfo
    {
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public int[] ActivityIds { get; set; }
        public int AdditionsCost { get; set; }
        public string InvoiceID { get; set; }
        public string InvoiceURL { get; set; }

        public bool Paid { get; set; }
        public Payment Payment { get; set; }
    }
}
