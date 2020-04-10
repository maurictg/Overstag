using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Text.Json;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
            => View();

        public IActionResult getUsers()
            => Json(new OverstagContext().Accounts.OrderBy(f => f.Firstname).ToList());

        [Route("Admin/loginAs/{token}")]
        public IActionResult loginAs(string token)
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.First(f => f.Token == Uri.UnescapeDataString(token));
                HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));
                HttpContext.Response.Redirect("/User");
                return Content("OK");
            }
        }

        public IActionResult saveType([FromQuery]int id, [FromQuery]byte type)
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.Find(id);
                user.Type = type;
                context.Accounts.Update(user);
                context.SaveChanges();
                return Json(new { status = "success" });
            }
        }

        public IActionResult InitDB()
        {
            try
            {
                new OverstagContext().Database.EnsureCreated();
                return Content("Database created!");
            }catch(Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
