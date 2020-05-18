using Microsoft.AspNetCore.Http;
using Overstag.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public int UserID { get; set; }
        public Payment Payment { get; set; }
        public int PaymentID;

        public InvoiceInfo toInvoiceInfo(string url = "/Pay/Invoice/")
        {
            return new InvoiceInfo()
            {
                AdditionsCost = (AdditionsCost / 100),
                Amount = (Amount / 100),
                Timestamp = Timestamp,
                ActivityIds = EventIDs.Split(',').Select(f => Convert.ToInt32(f)).ToArray(),
                InvoiceID = InvoiceID,
                InvoiceURL = url+Uri.EscapeDataString(InvoiceID),
                Payed = Payed,
                Payment = (Payed) ? Payment : null
            };
        }

        public bool IsPaid()
            => (Payment != null) ? (Payment.IsPaid() && Payed) : Payed;
    }

    //Xtended invoice
    public class XInvoice : Invoice
    {
        public Dictionary<Event, int> Events { get; set; }
    }
}
