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
        public string Date { get; set; }
        public string Time { get; set; }

        public int Cost { get; set; }
        public List<Participate> Participants { get; set; } = new List<Participate>();
    }

    //Tussentabel many2many relationship between Event and Account (user)
    public class Participate
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Account User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int Payed { get; set; }
    }
}
