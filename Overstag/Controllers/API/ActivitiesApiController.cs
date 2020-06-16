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
            List<Event> events = (after == null) 
                ? await new OverstagContext().Events.ToListAsync() 
                : await new OverstagContext().Events.Where(f => f.When > Convert.ToDateTime(after)).ToListAsync();

            return Json(new { status = "success", count = events.Count(), activities = events.Select(x => x.ToActivityInfo()) });
        }

        /// <summary>
        /// Get an activity by its ID
        /// </summary>
        /// <param name="id">The activity's id</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActivityById(int id)
        {
            var a = await new OverstagContext().Events.FindAsync(id);
            if (a == null)
                return Json(new { status = "error", error = "Activity not found" });
            else
                return Json(new { status = "success", activity = a.ToActivityInfo() });
        }

        /// <summary>
        /// List all activity-ids
        /// </summary>
        /// <param name="after">(Optional) show only ids of events after this date</param>
        /// <returns>JSON</returns>
        [HttpGet]
        [Route("ids")]
        public async Task<IActionResult> ListIds([FromQuery]DateTime? after)
        {
            List<int> ids = new List<int>();
            if (after == null)
                ids = await new OverstagContext().Events.Select(f => f.Id).ToListAsync();
            else
                ids = await new OverstagContext().Events.Where(f => f.When > Convert.ToDateTime(after)).Select(g => g.Id).ToListAsync();

            return Json(new { status = "success", count = ids.Count(), activities = ids });
        }

        [HttpGet]
        [Route("unfactured")]
        public async Task<IActionResult> GetUnfactured()
        {
            List<ActivityInfo> activities = new List<ActivityInfo>();
            var user = await new OverstagContext().Accounts.Include(f => f.Subscriptions).ThenInclude(g => g.Event).Include(f => f.Family).FirstAsync(f => f.Id == getUserId());
            foreach (var s in user.Subscriptions)
            {
                if (!s.Paid && !activities.Any(f => f.EventID == s.EventID) && Core.General.DateIsPassed(s.Event.When))
                    activities.Add(s.Event.ToActivityInfo());
            }

            return Json(new { status = "success", count = activities.Count(), activities });
        }
    }
}
