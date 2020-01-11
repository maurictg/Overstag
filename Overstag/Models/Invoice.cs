using System;
using System.Collections.Generic;

namespace Overstag.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventIDs { get; set; }
        public int AdditionsCost { get; set; }
        public bool Payed { get; set; }
        public string InvoiceID { get; set; }

        //Relations
        public Account User { get; set;  }
        public Payment Payment { get; set; }
        public int PaymentId;
    }

    //Xtended invoice
    public class XInvoice : Invoice
    {
        public Dictionary<Event, int> Events;
    }
}
