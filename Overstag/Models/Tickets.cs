using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int MinType { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
