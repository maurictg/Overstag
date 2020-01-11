using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult getUsers()
            => Json(new OverstagContext().Accounts.OrderBy(f => f.Firstname).ToList());

        public IActionResult InitDB()
        {
            try
            {
                return Content("Database created!");
            }catch(Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
