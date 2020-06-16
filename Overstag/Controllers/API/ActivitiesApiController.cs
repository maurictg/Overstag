using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.API;

namespace Overstag.Controllers.API
{
    [Route("api/activities")]
    public class ActivitiesApiController : OverstagApiController
    {
        /// <summary>
        /// Get all activities after date
        /// </summary>
        /// <param name="after">URL query param: ?after=[datetime yyyy-MM-dd... format]</param>
        /// <returns>JSON list with activities</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery]DateTime? after)
        {
            await using var context = new OverstagContext();
            List<Event> events = (after == null) 
                ? await context.Events.ToListAsync() 
                : await context.Events.Where(f => f.When > Convert.ToDateTime(after)).ToListAsync();

            return Json(new { status = "success", count = events.Count(), activities = events.Select(x => x.ToActivityInfo()) });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            await using var context = new OverstagContext();
            var a = await context.Events.FindAsync(id);
            if (a == null)
                return Json(new { status = "error", error = "Activity not found" });
            else
                return Json(new { status = "success", activity = a.ToActivityInfo() });
        }

        [HttpGet]
        [Route("ids")]
        public async Task<IActionResult> ListIds([FromQuery]DateTime? after)
        {
            await using var context = new OverstagContext();
            List<int> ids = new List<int>();
            if (after == null)
                ids = await context.Events.Select(f => f.Id).ToListAsync();
            else
                ids = await context.Events.Where(f => f.When > Convert.ToDateTime(after)).Select(g => g.Id).ToListAsync();

            return Json(new { status = "success", count = ids.Count(), activities = ids });
        }

        [HttpGet]
        [Route("unfactured")]
        public async Task<IActionResult> GetUnfactured()
        {
            await using var context = new OverstagContext();
            List<ActivityInfo> activities = new List<ActivityInfo>();
            var user = await context.Accounts.Include(f => f.Subscriptions).ThenInclude(g => g.Event).Include(f => f.Family).FirstAsync(f => f.Id == getUserId());
            foreach (var s in user.Subscriptions)
            {
                if (!s.Paid && activities.All(f => f.EventID != s.EventID) && Core.General.DateIsPassed(s.Event.When))
                    activities.Add(s.Event.ToActivityInfo());
            }

            return Json(new { status = "success", count = activities.Count(), activities });
        }
    }
}
