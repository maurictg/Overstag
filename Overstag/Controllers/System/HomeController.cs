using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        [Route("Home/Index")]
        public IActionResult Index() 
            => View();

        [Route("Home/About")]
        [Route("over-ons")]
        public IActionResult About() 
            => View();

        [Route("Home/Contact")]
        [Route("contact")]
        public IActionResult Contact() 
            => View();
        
        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>View with List(Event)</returns>
        [Route("Home/Events")]
        [Route("agenda")]
        public async Task<IActionResult> Events()
        {
            await using var context = new OverstagContext();
            var events = await context.Events.ToListAsync();
            ViewBag.Passed = events.Where(e => Core.General.DateIsPassed(e.When)).OrderByDescending(e => e.When).ToArray();
            return View(events.Where(e => !Core.General.DateIsPassed(e.When)).OrderBy(e => e.When).ToArray());
        }
    }
}