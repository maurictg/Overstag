using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;

namespace Overstag.Accountancy
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int? Type { get; set; }
        public DateTime When { get; set; }
        public string Description { get; set; }
    }

    public class Request
    {
        public int Id { get; set; }
        public DateTime When { get; set; }
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
        public bool Payed { get; set; }
        public string Metadata { get; set; } //JSON
        public string Description { get; set; }

        //Relations
        public Account User { get; set; }
    }
}
