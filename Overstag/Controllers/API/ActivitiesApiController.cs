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
            List<Event> events = new List<Event>();
            List<ActivityInfo> activities = new List<ActivityInfo>();

            if (after == null)
                events = await new OverstagContext().Events.ToListAsync();
            else
                events = await new OverstagContext().Events.Where(f => f.When > Convert.ToDateTime(after)).ToListAsync();

            events.ForEach(f => activities.Add(f.ToActivityInfo()));

            return Json(new { status = "success", count = activities.Count(), activities });
        }

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
    }
}
