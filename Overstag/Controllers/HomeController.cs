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
            => View(new OverstagContext().Events.ToList().Where(e => !Core.General.DateIsPassed(e.When)).OrderBy(e => e.When).ToArray());

    }
}