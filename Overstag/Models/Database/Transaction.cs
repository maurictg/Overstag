using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.Database
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Billed { get; set; }
        public string Metadata { get; set; } //JSON
    }
}
