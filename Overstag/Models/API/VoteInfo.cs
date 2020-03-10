using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Models.API
{
    public class VoteInfo
    {
        public IdeaInfo Idea { get; set; }
        public int IdeaID { get; set; }
        public bool Vote { get; set; }
    }

    public class IdeaInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cost { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
    }
}
