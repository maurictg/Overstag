using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System;

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


        [Route("Home/Events")]
        [Route("agenda")]
        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>List(Event)</returns>
        public IActionResult Events()
        {
            var events = new OverstagContext().Events.ToList();
            ViewBag.Passed = events.Where(e => Core.General.DateIsPassed(e.When)).OrderBy(e => e.When).ToArray().Reverse().ToArray();
            return View(events.Where(e => !Core.General.DateIsPassed(e.When)).OrderBy(e => e.When).ToArray());
        }
            

    }
}