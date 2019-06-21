using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(){ return View(); }

        /// <summary>
        /// Get all events from the database
        /// </summary>
        /// <returns>List(Event)</returns>
        public IActionResult Events()
        {
            using (var context = new OverstagContext())
            {
                //Return sorted list
                return View(context.Events.OrderBy(e => e.When).ToList());
            }
        }
        
    }
}