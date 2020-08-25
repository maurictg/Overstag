﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Controllers.API
{
    /*
    [Route("api/ideas")]
    public class IdeaApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List()
        {
            await using var context = new OverstagContext();
            List<IdeaVoteInfo> ideai = new List<IdeaVoteInfo>();
            var ideas = await context.Ideas.Include(f => f.Votes).ToListAsync();
            var userVotes = await context.Accounts.Include(f => f.Votes).FirstOrDefaultAsync(g => g.Id == getUserId());

            foreach (var item in ideas)
            {
                var vote = userVotes.Votes.FirstOrDefault(f => f.IdeaID == item.Id);
                ideai.Add(new IdeaVoteInfo()
                {
                    Idea = new IdeaInfo()
                    {
                        Id = item.Id,
                        Cost = item.Cost,
                        Description = item.Description,
                        Title = item.Title,
                        Downvotes = item.Votes.Count(f => !f.Upvote),
                        Upvotes = item.Votes.Count(f => f.Upvote)
                    },
                    Vote = (vote == null) ? null : new VoteInfo()
                    {
                        IdeaID = vote.IdeaID,
                        Vote = vote.Upvote
                    }
                });
            }

            return Json(new { status = "success", count = ideas.Count(), ideas = ideai.OrderByDescending(f => (f.Idea.Upvotes - f.Idea.Downvotes)).ToList() });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetIdeaById(int id)
        {
            await using var context = new OverstagContext();
            var a = await context.Ideas.FindAsync(id);
            if (a == null)
                return Json(new { status = "error", error = "Idea not found" });
            else
                return Json(new { status = "success", idea = a.ToIdeaInfo() });
        }
    }*/
}
