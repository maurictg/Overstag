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
        public byte Type { get; set; } //0: chill, 1: activity

        //Relations
        public List<Participate> Participators { get; set; }

        public API.ActivityInfo ToActivityInfo()
        {
            return new API.ActivityInfo
            {
                EventID = Id,
                Cost = Math.Round((double)Cost/100, 2),
                Description = Description,
                Title = Title,
                When = When
            };
        }
    }

    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }

        //Relations
        public List<Vote> Votes { get; set; }

        public API.IdeaInfo ToIdeaInfo()
        {
            return new API.IdeaInfo
            {
                Cost = Cost,
                Description = Description,
                Title = Title,
                Id = Id
            };
        }
    }

    // [Intermediate table]
    public class Vote
    {
        public int IdeaID { get; set; }
        public Idea Idea { get; set; }
        public int UserID { get; set; }
        public Account User { get; set; }
        public bool Upvote { get; set; }

        public API.VoteInfo ToVoteInfo(bool withIdea = false)
        {
            API.IdeaInfo i = (Idea == null) ? null : Idea.ToIdeaInfo();

            return new API.VoteInfo
            {
                IdeaID = IdeaID,
                Vote = Upvote,
            };
        }
    }

    // [Intermediate table]
    public class Participate
    {
        //Relations
        public int UserID { get; set; }
        public Account User { get; set; }
        public int EventID { get; set; }
        public Event Event { get; set; }
        public bool Paid { get; set; }

        public int AdditionsCost { get; set; }
        public byte FriendCount { get; set; }

        public API.SubscriptionInfo ToSubscriptionInfo(bool withActivity = false)
        {
            return new API.SubscriptionInfo
            {
                Activity = (!withActivity || Event == null) ? null : Event.ToActivityInfo(),
                Factured = Paid,
                FriendCount = FriendCount,
                DrinksCost = Math.Round((double)AdditionsCost / 100, 2),
                EventID = EventID
            };
        }
    }
}
