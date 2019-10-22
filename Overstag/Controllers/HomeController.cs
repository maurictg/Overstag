using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System;

namespace Overstag.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() 
            => View();

        public IActionResult About() 
            => View();

        public IActionResult Contact() 
            => View();


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