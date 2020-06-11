using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Authorization;
using Overstag.Models;
using Overstag.Models.API;

namespace Overstag.Controllers.API
{
    [OverstagAuthorize]
    [Route("api/votes")]
    public class VotesApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
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

        [HttpGet]
        [Route("{id}/upvote")]
        public async Task<IActionResult> Upvote(int id)
        {
            if (getUser().Type == 1)
                return Json(new { status = "warning", message = "Parent is not allowed to vote" });
            else
            {
                if (!new OverstagContext().Ideas.Any(f => f.Id == id))
                    return Json(new { status = "error", error = "Idea not found." });

                string res = await Vote(id, true);
                if (res == "OK")
                    return Json(new { status = "success" });
                else
                    return Json(new { status = "error", error = "Something went wrong" });
            }
        }

        [HttpGet]
        [Route("{id}/downvote")]
        public async Task<IActionResult> Downvote(int id)
        {
            if (getUser().Type == 1)
                return Json(new { status = "warning", message = "Parent is not allowed to vote" });
            else
            {
                if (!new OverstagContext().Ideas.Any(f => f.Id == id))
                    return Json(new { status = "error", error = "Idea not found." });

                string res = await Vote(id, false);
                if (res == "OK")
                    return Json(new { status = "success" });
                else
                    return Json(new { status = "error", error = "Something went wrong" });
            }
        }

        private async Task<string> Vote(int id, bool upvote)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Votes).First(u => u.Id == getUserId());

                    if (user.Votes.Any(u => u.IdeaID == id))
                        user.Votes.First(u => u.IdeaID == id).Upvote = upvote;
                    else
                    {
                        user.Votes.Add(new Models.Vote
                        {
                            IdeaID = id,
                            UserID = user.Id,
                            Upvote = upvote
                        });
                    }
                    context.Accounts.Update(user);
                    await context.SaveChangesAsync();
                    return "OK";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
    }
}
