using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overstag.Models;
using Overstag.Models.NoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Controllers
{
    public class MentorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Today()
        {
            using (var context = new OverstagContext())
            {
                var now = context.Events.Include(f => f.Participators).OrderBy(h => h.When).FirstOrDefault(g => g.When.Date == DateTime.Today);
                List<Account> Users = new List<Account>();

                if(now != null)
                {
                    foreach (var p in now.Participators)
                        Users.Add(context.Accounts.First(f => f.Id == p.UserID));
                }
                
                return View(new UserEvent
                {
                    Event = now,
                    Participators = Users
                });
            }
        }

        [HttpPost]
        public IActionResult postPresence([FromForm]string absentids)
        {
            int[] absentIDS = JsonConvert.DeserializeObject<int[]>(absentids);
            
            using(var context = new OverstagContext())
            {
                var eve = context.Events.Include(f => f.Participators).FirstOrDefault(f => f.When.Date == DateTime.Today);
                
                if(eve != null)
                {
                    foreach (int id in absentIDS)
                    {
                        var parti = eve.Participators;
                        var e = eve.Participators.First(f => f.UserID == id);
                        if (e.Payed == 0)
                        {
                            eve.Participators.Remove(e);
                        }
                    }

                    try
                    {
                        context.Events.Update(eve);
                        context.SaveChanges();
                        return Json(new { status = "success" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { status = "error", debuginfo = e });
                    }
                }
                else
                {
                    return Json(new { status = "warning", warning = "Er is geen activiteit vandaag" });
                }
            }
        }

        [HttpGet("/Mentor/addDrink/{userid}")]
        public IActionResult addDrink(int userid)
        {
            using(var context = new OverstagContext())
            {
                var eve = context.Events.Include(f => f.Participators).FirstOrDefault(f => f.When.Date == DateTime.Today);

                if (eve == null)
                    return Json(new { status = "error", error = "Er is geen activiteit vandaag" });

                try
                {
                    var user = eve.Participators.First(f => f.UserID == userid);
                    user.ConsumptionCount++;
                    user.ConsumptionTax += 100;
                    context.Events.Update(eve);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is iets fout gegaan", debuginfo = e.Message });
                }
                
            }
        }
    }
}
