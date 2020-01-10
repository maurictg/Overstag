using System;
using System.Collections.Generic;

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

    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }

        //Relations
        public List<Vote> Votes { get; set; }
    }

    // [Intermediate table]
    public class Vote
    {
        public int IdeaID { get; set; }
        public Idea Idea { get; set; }
        public int UserID { get; set; }
        public Account User { get; set; }
        public bool Upvote { get; set; }
    }

    // [Intermediate table]
    public class Participate
    {
        //Relations
        public int UserID { get; set; }
        public Account User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }

        public bool Payed { get; set; }

        public int AdditionsCost { get; set; }
        public int FriendCount { get; set; }
    }
}
