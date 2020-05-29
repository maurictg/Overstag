using Microsoft.AspNetCore.Http;
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
    [Route("api/subscriptions")]
    public class SubscriptionsApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery]bool withFactured, [FromQuery]bool withActivity, [FromQuery]DateTime? after)
        {
            using (var context = new OverstagContext())
            {
                List<SubscriptionInfo> inf = new List<SubscriptionInfo>();
                var user = await context.Accounts.Include(f => f.Subscriptions).ThenInclude(h => h.Event).FirstOrDefaultAsync(g => g.Id == getUserId());

                foreach (var item in user.Subscriptions)
                {
                    if (item.Paid && !withFactured)
                        continue;

                    if (after != null && Core.General.DateIsPassed(item.Event.When))
                        continue;

                    inf.Add(item.ToSubscriptionInfo(withActivity));
                }

                return Json(new { status = "success", count = inf.Count(), subscriptions = inf });
            }
        }

        [HttpPost]
        [Route("add")]
        [Route("subscribe")]
        public async Task<IActionResult> Add([FromForm]int eventId, [FromForm]byte? friendCount)
        {
            using (var context = new OverstagContext())
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstOrDefaultAsync(g => g.Id == getUserId());
                var eve = await context.Events.FindAsync(eventId);

                if (eve == null)
                {
                    HttpContext.Response.ContentType = "application/json";
                    return StatusCode(StatusCodes.Status404NotFound, new { status = "error", error = "Event not found." });
                }

                if (DateTime.Today > eve.When.Date)
                    return Json(new { status = "error", error = "Can't subscribe to activity that already passed.", dutchError = "Deze activiteit is al voorbij. U kunt zich hiervoor niet meer inschrijven" });

                var part = user.Subscriptions.Where(e => e.EventID == eventId).FirstOrDefault();
                if (part == null)
                {
                    var subscription = new Participate { UserID = user.Id, EventID = eve.Id, FriendCount = (friendCount != null) ? Convert.ToByte(friendCount) : (byte)0 };
                    user.Subscriptions.Add(subscription);
                    await context.SaveChangesAsync();

                    return Json(new { status = "success", subscription = subscription.ToSubscriptionInfo() });
                }
                else
                    return Json(new { status = "warning", error = "Can't subscribe: subscription already exist", dutchError = "Je bent al ingeschreven voor deze activiteit" });
            }
        }

        [HttpPost]
        [Route("remove")]
        [Route("unsubscribe")]
        public async Task<IActionResult> Remove([FromForm]int eventId)
        {
            using (var context = new OverstagContext())
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstOrDefaultAsync(g => g.Id == getUserId());
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
    }
}
