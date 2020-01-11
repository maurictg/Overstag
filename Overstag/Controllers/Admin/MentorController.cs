using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Overstag.Models;
using Overstag.Models.NoDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Controllers
{
    public class MentorController : Controller
    {
        /// <summary>
        /// Get Mentor homepage
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.Token = HttpContext.Session.GetString("Token");
            return View();
        }

        /// <summary>
        /// Get event and subscription of the current event
        /// </summary>
        /// <returns>View with info</returns>
        [Route("Mentor/Today")]
        [Route("Mentor/Event/{eventid}")]
        public IActionResult Event(int? eventid)
        {
            using (var context = new OverstagContext())
            {
                int eid = -1;
                try
                {
                    eid = context.Events.FirstOrDefault(f => f.When.Date == DateTime.Today).Id;
                }
                catch { }

                eid = (eventid == null) ? eid : Convert.ToInt32(eventid);
                var now = context.Events.Include(f => f.Participators).OrderBy(h => h.When).FirstOrDefault(g => g.Id == eid);

                List<SSub> Users = new List<SSub>();

                if(now != null)
                {
                    if (now.When < DateTime.Now.AddDays(-7))
                    {
                        string[] error = { "Geen toegang.", "Deze activiteit was al meer dan een week geleden. <br/>Het is niet toegestaan om nu nog wijzigingen aan te brengen." };
                        return View("~/Views/Error/Custom.cshtml", error);
                    }

                    if(now.When.Date > DateTime.Now.Date)
                    {
                        string[] error = { "Geen toegang.", "Deze activiteit is in de toekomst. <br/>Het is niet toegestaan wijzigingen van te voren aan te brengen." };
                        return View("~/Views/Error/Custom.cshtml", error);
                    }

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

        /// <summary>
        /// Get statistics about subscriptions
        /// </summary>
        /// <returns>View with graphs and info</returns>
        public IActionResult Stats()
        {
            using (var context = new OverstagContext())
            {
                var events = context.Events.Include(f => f.Participators).OrderBy(f => f.When).ToList();
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

        /// <summary>
        /// Deletes all subscriptions from absent people
        /// </summary>
        /// <param name="absentids">Array with absent ids (json)</param>
        /// <param name="eventID">The event's identifier</param>
        /// <returns>JSON, success error or warning</returns>
        [HttpPost]
        public async Task<IActionResult> postPresence([FromForm]string absentids, [FromForm]int eventID)
        {
            int[] absentIDS = JsonSerializer.Deserialize<int[]>(absentids);
            
            using(var context = new OverstagContext())
            {
                var eve = context.Events.Include(f => f.Participators).FirstOrDefault(f => f.Id == eventID);
                
                if(eve != null)
                {
                    foreach (int id in absentIDS)
                    {
                        var parti = eve.Participators.ToList();
                        var e = eve.Participators.First(f => f.UserID == id);
                        if (!e.Payed)
                        {
                            eve.Participators.Remove(e);
                        }
                    }

                    try
                    {
                        context.Events.Update(eve);
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { status = "error", debuginfo = e });
                    }
                }
                else
                {
                    return Json(new { status = "warning", warning = "Activiteit niet gevonden" });
                }
            }
        }

        /// <summary>
        /// Set drink amount for user
        /// </summary>
        /// <param name="userid">The user's id</param>
        /// <param name="count">The amount of drinks</param>
        /// <param name="eventid">The event's identifier</param>
        /// <returns>JSON, success or error</returns>
        [HttpGet("/Mentor/setDrink/{eventid}/{userid}/{amount}")]
        public async Task<IActionResult> setDrink(int eventid, int userid, int amount)
        {
            using(var context = new OverstagContext())
            {
                var eve = context.Events.Include(f => f.Participators).FirstOrDefault(f => f.Id == eventid);

                if (eve == null)
                    return Json(new { status = "error", error = "Activiteit niet gevonden" });

                try
                {
                    var user = eve.Participators.First(f => f.UserID == userid);
                    if (user.Payed)
                        return Json(new { status = "error", error = "Gebruiker heeft al betaald" });

                    user.AdditionsCost = amount;
                    context.Events.Update(eve);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is iets fout gegaan", debuginfo = e.Message });
                }
                
            }
        }

        [HttpGet("Mentor/addUser/{eventid}/{userid}")]
        public async Task<IActionResult> addUser(int eventid, int userid)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Subscriptions).First(a => a.Id == userid);
                    var eve = context.Events.First(e => e.Id == eventid);

                    if (DateTime.Today > eve.When.Date.AddDays(3))
                        return Json(new { status = "error", error = "Het is nu te lang geleden. U kunt geen mensen meer inschrijven." });

                    if (user.Subscriptions.Any(e => e.EventID == eventid))
                    {
                        user.Subscriptions.Add(new Participate { UserID = user.Id, EventID = eve.Id });
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    else
                        return Json(new { status = "error", error = "Deze gebruiker is al ingeschreven!" });
                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                }

            }
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>View with users</returns>
        public IActionResult Users()
            => View(new OverstagContext().Accounts.Where(f => f.Type == 0).OrderBy(g => g.Firstname).ToList());

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>View with events</returns>
        public IActionResult Events()
            => View(new OverstagContext().Events.OrderBy(e => e.When).ToList());

        /// <summary>
        /// Get event in JSON by ID
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <returns>Status = success and data: Event object in JSON</returns>
        [HttpGet("/Mentor/getEvent/{id}")]
        public JsonResult GetEvent(int id)
            => Json(new { status = "success", data = new OverstagContext().Events.First(f => f.Id == id) });

        /// <summary>
        /// Add an event to the database
        /// </summary>
        /// <param name="e">The event form object</param>
        /// <returns>Json: status = success or status = error</returns>
        [HttpPost]
        public async Task<IActionResult> postEvent(Event e)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var eve = new Event();

                    if (e.Id != -1)
                        eve = context.Events.First(f => f.Id == e.Id);

                    eve.Title = e.Title;
                    eve.Description = e.Description;
                    eve.When = e.When;
                    eve.Cost = e.Cost;

                    if (e.Id == -1)
                        context.Events.Add(eve);
                    else
                        context.Events.Update(eve);

                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", error = "Er is iets fout gegaan", debuginfo = ex.ToString() });
            }
        }

        /// <summary>
        /// Delete an event by it's id
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpGet("/Mentor/deleteEvent/{id}")]
        public async Task<IActionResult> deleteEvent(int id)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var e = context.Events.First(f => f.Id == id);
                    context.Events.Remove(e);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception ex)
            {
                return Json(new { status = "error", error = "Verwijderen mislukt", debuginfo = ex.ToString() });
            }
        }

        /// <summary>
        /// Get all ideas sorted by votes
        /// </summary>
        /// <returns>View with sorted ideas</returns>
        public IActionResult Votes()
            => View(new OverstagContext().Ideas.Include(f => f.Votes).OrderBy(b => (b.Votes.Count(i => i.Upvote) - b.Votes.Count(i => !i.Upvote))).ToArray().Reverse().ToList());

        /// <summary>
        /// Delete an idea
        /// </summary>
        /// <param name="id">The idea its id</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpGet("Mentor/deleteVote/{id}")]
        public async Task<IActionResult> deleteVote(int id)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var idea = context.Ideas.First(i => i.Id == id);
                    context.Ideas.Remove(idea);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = e.ToString() });
                }
            }
        }

    }
}
