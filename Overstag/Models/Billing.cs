using System;
using System.Collections.Generic;
using Overstag.Models;

namespace Overstag.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventIDs { get; set; }
        public int AdditionsCount { get; set; }
        public int Payed { get; set; }
        public string PayID { get; set; }
    }
}
