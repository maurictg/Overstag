using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.API;

namespace Overstag.Controllers.API
{
    [Route("api/vote")]
    public class VotesApiController : OverstagApiController
    {
        [HttpGet]
        [Route("{id}/up")]
        public async Task<IActionResult> Upvote(int id)
        {
            if (getUser().Type == 1)
                return Json(new { status = "warning", message = "Parent is not allowed to vote" });
            else
            {
                string res = await Vote(id, true);
                if (res == "OK")
                    return Json(new { status = "success" });
                else
                    return Json(new { status = "error", error = "Something went wrong" });
            }
        }

        [HttpGet]
        [Route("{id}/down")]
        public async Task<IActionResult> Downvote(int id)
        {
            if (getUser().Type == 1)
                return Json(new { status = "warning", message = "Parent is not allowed to vote" });
            else
            {
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
