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

        //Relations
        public List<Participate> Participators { get; set; }
    }

    // [Intermediate table]
    public class Participate
    {
        //Relations
        public int UserID { get; set; }
        public Account User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }

        public int Payed { get; set; }
    }
}
