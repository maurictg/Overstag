using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Accountancy
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime When { get; set; }
        public string Description { get; set; }
    }

    public class Request
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime When { get; set; }
        public DateTime Timestamp { get; set; }
        public int Amount { get; set; }
        public int Payed { get; set; }
        //public int isGift { get; set; }
        public string Metadata { get; set; } //JSON
        public string Description { get; set; }
    }
}
