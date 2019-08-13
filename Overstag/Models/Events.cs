using System;
using System.Collections.Generic;
using Overstag.Models;

namespace Overstag.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public int Cost { get; set; }
    }

    public class Participate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Payed { get; set; }
    }
}
