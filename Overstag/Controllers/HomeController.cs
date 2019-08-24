using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using System;

namespace Overstag.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Contact() => View();


        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>List(Event)</returns>
        public IActionResult Events()
        {
            using (var context = new OverstagContext())
            {
                //Return sorted list
                return View(context.Events.Where(e => e.When > DateTime.Now).OrderBy(e => e.When).ToArray());
            }
        }
        
    }
}