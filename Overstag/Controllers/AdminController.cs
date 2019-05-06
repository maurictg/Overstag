using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using Microsoft.AspNetCore.Mvc;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string InitDB()
        {
            using(var context = new Overstag.Models.OverstagContext())
            {
                try
                {
                    context.Database.EnsureCreatedAsync();
                    return "Calling: Admin/initDB.\n\nDatabase successfully restored!!";
                }catch(Exception ex)
                {
                    return "Calling: Admin/initDB.\n\nError: " + ex.ToString();
                }
            }
        }

        public string DeleteDB()
        {
            using (var context = new Overstag.Models.OverstagContext())
            {
                try
                {
                    context.Database.EnsureDeletedAsync();
                    return "Calling: Admin/deleteDB.\n\nDatabase successfully deleted!!";
                }
                catch (Exception ex)
                {
                    return "Calling: Admin/deleteDB.\n\nError: " + ex.ToString();
                }
            }
        }

        public IActionResult Events()
        {
            using(var context = new OverstagContext())
            {
                List<Event> events = context.Events.ToList();
                return View(events);
            }
        }

        [HttpPost]
        public async Task<IActionResult> postEvent(Event e)
        {
            if (ModelState.IsValid)
            {
                using (var context = new OverstagContext())
                {
                     try
                     {
                         Event a = new Event();
                         a.Title = e.Title; a.Description = e.Description; a.Date = e.Date; a.Time = e.Time; a.Cost = e.Cost; 
                         context.Add(a);
                         await context.SaveChangesAsync();
                         return Json(new { status = "success" });
                     }
                     catch (Exception ex)
                     {
                         return Json(new { status = "error", error = "POST is mislukt door interne fout.\nProbeer het later opnieuw.", debuginfo = ex.ToString() });
                     }
                }

            }
            else { return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" }); }
        }

        [HttpPost]
        public async Task<IActionResult> deleteEvent(int Id)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    Event a = context.Events.Where(e => e.Id.Equals(Id)).FirstOrDefault();
                    context.Remove(a);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", error = ex.ToString() });
                }
            }
        }
    }
}