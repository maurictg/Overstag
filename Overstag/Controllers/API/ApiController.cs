using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overstag.Models;
using Overstag.Models.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Controllers.API
{
    public class ApiController : Controller
    {
        private async Task<Auth> GetCurrent()
        {
            if (!HttpContext.Request.Headers.ContainsKey("Token"))
                return null;

            string token = HttpContext.Request.Headers["Token"];

            using (var context = new OverstagContext())
            {
                var auth = await context.Auths.Include(f => f.User).FirstOrDefaultAsync(g => g.Token == token);
                if (auth != null && auth.IP == "OVERSTAG_APP")
                    return auth;
                else
                    return null;
            }
        }

        [HttpGet("api/GetActivities")]
        public async Task<JsonResult> GetActivities([FromQuery]DateTime? after)
        {
            List<Event> events = new List<Event>();
            List<ActivityInfo> activities = new List<ActivityInfo>();

            if (after == null)
                events = await new OverstagContext().Events.ToListAsync();
            else
                events = await new OverstagContext().Events.Where(f => f.When > Convert.ToDateTime(after)).ToListAsync();

            events.ForEach(f => activities.Add(f.ToActivityInfo()));

            return Json(new { status = "success", count = activities.Count(), activities });
        }

        [Produces("application/json")]
        public async Task<IActionResult> GetUserInfo()
        {
            var current = await GetCurrent();
            if (current == null) return StatusCode(StatusCodes.Status401Unauthorized, new { status = "error", error = "Authentication error." });

            return Json(new { status = "success", data = current.User.ToUserInfo() });
        }

        [HttpGet("api/GetSubscriptions")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSubscriptions([FromQuery]bool withFactured, [FromQuery]bool withActivity)
        {
            var current = await GetCurrent();
            if (current == null) return StatusCode(StatusCodes.Status401Unauthorized, new { status = "error", error = "Authentication error." });

            using(var context = new OverstagContext())
            {
                List<SubscriptionInfo> inf = new List<SubscriptionInfo>();
                var user = context.Accounts.Include(f => f.Subscriptions).ThenInclude(h => h.Event).FirstOrDefault(g => g.Id == current.UserId);

                foreach (var item in user.Subscriptions)
                {
                    if (item.Payed && !withFactured)
                        continue;

                    inf.Add(item.ToSubscriptionInfo(withActivity));
                }

                return Json(new { status = "success", count = inf.Count(), subscriptions = inf });
            }
        }

        [HttpGet("api/Subscribe/{eventID}")]
        [Produces("application/json")]
        public async Task<IActionResult> Subscribe(int eventId, [FromQuery]byte? friendCount)
        {
            var current = await GetCurrent();
            if (current == null) return StatusCode(StatusCodes.Status401Unauthorized, new { status = "error", error = "Authentication error." });

            using (var context = new OverstagContext())
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstOrDefaultAsync(g => g.Id == current.UserId);
                var eve = await context.Events.FindAsync(eventId);

                if(eve == null) return StatusCode(StatusCodes.Status404NotFound, new { status = "error", error = "Event not found." });

                if (DateTime.Today > eve.When.Date)
                    return Json(new { status = "error", error = "Can't subscribe to activity that already passed.", dutchError = "Deze activiteit is al voorbij. U kunt zich hiervoor niet meer inschrijven" });

                var part = user.Subscriptions.Where(e => e.EventID == eventId).FirstOrDefault();
                if (part == null)
                {
                    user.Subscriptions.Add(new Participate { UserID = user.Id, EventID = eve.Id, FriendCount = (friendCount != null) ? Convert.ToByte(friendCount) : (byte)0 });
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                else
                    return Json(new { status = "warning", error = "Can't subscribe: subscription already exist", dutchError = "Je bent al ingeschreven voor deze activiteit" });
            }
        }

        [HttpGet("api/Unsubscribe/{eventID}")]
        [Produces("application/json")]
        public async Task<IActionResult> Unsubscribe(int eventId)
        {
            var current = await GetCurrent();
            if (current == null) return StatusCode(StatusCodes.Status401Unauthorized, new { status = "error", error = "Authentication error." });

            using (var context = new OverstagContext())
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstOrDefaultAsync(g => g.Id == current.UserId);
                var sub = user.Subscriptions.FirstOrDefault(f => f.EventID == eventId);
                var eve = await context.Events.FindAsync(eventId);

                if (sub == null) 
                    return Json(new { status = "error", error = "Subscription does not exist" });

                if (Core.General.DateIsPassed(eve.When))
                    return Json(new { status = "error", error = "Activity already passed. Can't unsubscribe.", dutchError = "Dit event is al voorbij. U kunt zich hiervoor niet meer uitschrijven" });

                user.Subscriptions.Remove(sub);
                await context.SaveChangesAsync();
                return Json(new { status = "success" });
            }
        }

        public async Task<JsonResult> GetIdeas()
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

        [Produces("application/json")]
        public async Task<IActionResult> GetVotes([FromQuery]bool withIdeas)
        {
            var current = await GetCurrent();
            if (current == null) return StatusCode(StatusCodes.Status401Unauthorized, new { status = "error", error = "Authentication error." });

            using (var context = new OverstagContext())
            {
                List<Idea> ideas = await context.Ideas.ToListAsync();

                var user = await context.Accounts.Include(f => f.Votes).ThenInclude(h => h.Idea).FirstOrDefaultAsync(g => g.Id == current.UserId);
                List<VoteInfo> votes = new List<VoteInfo>();
                user.Votes.ForEach(f => votes.Add(f.ToVoteInfo(withIdeas)));

                return Json(new { status = "success", count = votes.Count, votes });
            }
        }
    }
}