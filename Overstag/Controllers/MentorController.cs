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
                List<SSub> Users = new List<SSub>();

                if(now != null)
                {
                    foreach (var p in now.Participators)
                        Users.Add(new SSub { account = context.Accounts.First(f => f.Id == p.UserID), part = p});
                }
                
                return View(new UserEvent
                {
                    Event = now,
                    Participators = Users
                });
            }
        }

        public IActionResult Stats()
        {
            using (var context = new OverstagContext())
            {
                var events = context.Events.Include(f => f.Participators).OrderBy(f => f.When);
                List<SSubEvent> sse = new List<SSubEvent>();
                foreach (var e in events)
                {
                    List<SSub> subs = new List<SSub>();
                    foreach (var s in e.Participators)
                        subs.Add(new SSub
                        {
                            account = context.Accounts.First(f => f.Id == s.UserID),
                            part = s
                        });

                    sse.Add(new SSubEvent()
                    {
                        Event = e,
                        Sub = subs.OrderBy(f => f.account.Lastname).ToList()
                    });
                }

                return View(sse);
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

        [HttpGet("/Mentor/setDrink/{userid}/{count}")]
        public IActionResult setDrink(int userid, int count)
        {
            using(var context = new OverstagContext())
            {
                var eve = context.Events.Include(f => f.Participators).FirstOrDefault(f => f.When.Date == DateTime.Today);

                if (eve == null)
                    return Json(new { status = "error", error = "Er is geen activiteit vandaag" });

                try
                {
                    var user = eve.Participators.First(f => f.UserID == userid);
                    user.ConsumptionCount = (count >= 0) ? count : 0;
                    user.ConsumptionTax = user.ConsumptionCount * 100;
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
