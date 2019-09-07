using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Overstag.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
