using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Controllers.API
{
    [Route("api/ideas")]
    public class IdeaApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List()
        {
            List<IdeaInfo> ideai = new List<IdeaInfo>();
            var ideas = await new OverstagContext().Ideas.Include(f => f.Votes).ToListAsync();

            foreach (var item in ideas)
            {
                ideai.Add(new IdeaInfo()
                {
                    Id = item.Id,
                    Cost = item.Cost,
                    Description = item.Description,
                    Title = item.Title,
                    Downvotes = item.Votes.Count(f => !f.Upvote),
                    Upvotes = item.Votes.Count(f => f.Upvote)
                });
            }

            return Json(new { status = "success", count = ideas.Count(), ideas = ideai.OrderByDescending(f => (f.Upvotes - f.Downvotes)).ToList() });
        }

        public async Task<IActionResult> ListVotes([FromQuery]bool withIdeas)
        {
            using (var context = new OverstagContext())
            {
                List<Idea> ideas = await context.Ideas.ToListAsync();

                var user = await context.Accounts.Include(f => f.Votes).ThenInclude(h => h.Idea).FirstOrDefaultAsync(g => g.Id == getUserId());
                List<VoteInfo> votes = new List<VoteInfo>();
                user.Votes.ForEach(f => votes.Add(f.ToVoteInfo(withIdeas)));

                return Json(new { status = "success", count = votes.Count, votes });
            }
        }
    }
}
