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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;
using Overstag.Authorization;

namespace Overstag.Controllers
{
    [OverstagAuthorize(2, 3)]
    public class MentorController : OverstagController
    {
        /// <summary>
        /// Get Mentor homepage
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Index()
        {
            ViewBag.Token = currentUser.Token;
            return View();
        }

        /// <summary>
        /// Get event and subscription of the current event
        /// </summary>
        /// <returns>View with info</returns>
        [Route("Mentor/Today")]
        [Route("Mentor/Event/{eventid}")]
        public async Task<IActionResult> Event(int? eventid)
        {
            await using var context = new OverstagContext();
            int eid = (await context.Events.FirstOrDefaultAsync(f => f.When.Date == DateTime.Today))?.Id??-1;

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
                    Users.Add(new SSub { account = await context.Accounts.FirstAsync(f => f.Id == p.UserID), part = p});
            }
                
            return View(new UserEvent
            {
                Event = now,
                Participators = Users
            });
        }

        /// <summary>
        /// Get statistics about subscriptions
        /// </summary>
        /// <returns>View with graphs and info</returns>
        public async Task<IActionResult> Stats()
        {
            await using var context = new OverstagContext();
            var events = await context.Events.Include(f => f.Participators).OrderBy(f => f.When).ToListAsync();
            List<SSubEvent> sse = new List<SSubEvent>();
            foreach (var e in events)
            {
                List<SSub> subs = new List<SSub>();
                foreach (var s in e.Participators)
                {
                    SSub sssb = new SSub
                    {
                        account = await context.Accounts.FirstAsync(f => f.Id == s.UserID),
                        part = s
                    };

                    if (sssb.account.Type == 0)
                        subs.Add(sssb);
                }

                sse.Add(new SSubEvent()
                {
                    Event = e,
                    Sub = subs.OrderBy(f => f.account.Lastname).ToList()
                });
            }

            return View(sse);
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

            await using var context = new OverstagContext();
            var eve = await context.Events.Include(f => f.Participators).FirstOrDefaultAsync(f => f.Id == eventID);
                
            if(eve != null)
            {
                foreach (int id in absentIDS)
                {
                    var e = eve.Participators.First(f => f.UserID == id);
                    if (!e.Paid)
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
            await using var context = new OverstagContext();
            var eve = await context.Events.Include(f => f.Participators).FirstOrDefaultAsync(f => f.Id == eventid);

            if (eve == null)
                return Json(new { status = "error", error = "Activiteit niet gevonden" });

            try
            {
                var user = eve.Participators.First(f => f.UserID == userid);
                if (user.Paid)
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

        /// <summary>
        /// Add user to an event
        /// </summary>
        /// <param name="eventid">The eventID</param>
        /// <param name="userid">The user's ID to be added</param>
        /// <returns>JSON(status = success or error)</returns>
        [HttpGet("Mentor/addUser/{eventid}/{userid}")]
        public async Task<IActionResult> addUser(int eventid, int userid)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var user = await context.Accounts.Include(f => f.Subscriptions).FirstAsync(a => a.Id == userid);
                    var eve = context.Events.First(e => e.Id == eventid);

                    if (DateTime.Today > eve.When.Date.AddDays(3))
                        return Json(new { status = "error", error = "Het is nu te lang geleden. U kunt geen mensen meer inschrijven." });

                    if (user.Subscriptions.All(e => e.EventID != eventid))
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
        public async Task<IActionResult> Users()
        {
           await using var context = new OverstagContext();
           return View(await context.Accounts.Where(f => f.Type == 0).OrderBy(g => g.Firstname).ToListAsync());
        }

        /// <summary>
        /// Get all events
        /// </summary>
        /// <returns>View with events</returns>
        public async Task<IActionResult> Events()
        {
            await using var context = new OverstagContext();
            return View(await context.Events.OrderBy(e => e.When).ToListAsync());
        }

        /// <summary>
        /// Get event in JSON by ID
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <returns>Status = success and data: Event object in JSON</returns>
        [HttpGet("/Mentor/getEvent/{id}")]
        public async Task<JsonResult> GetEvent(int id)
        {
            await using var context = new OverstagContext();
            return Json(new { status = "success", data = await context.Events.FirstOrDefaultAsync(f => f.Id == id) }); 
        }

        /// <summary>
        /// Add an event to the database
        /// </summary>
        /// <param name="e">The event form object</param>
        /// <returns>Json: status = success or status = error</returns>
        [HttpPost]
        public async Task<IActionResult> postEvent([FromForm]int id, [FromForm]string title, [FromForm]string description, [FromForm]string when, [FromForm]int cost)
        {
            try
            {
                await using var context = new OverstagContext();
                var eve = new Event();

                    if (id != -1)
                    {
                        eve = await context.Events.FindAsync(id);
                        eve.Id = id;
                    }

                    eve.Title = title;
                    eve.Description = description;
                    eve.When = DateTime.ParseExact(when, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture);
                    eve.Cost = cost;
                    eve.Type = (byte)(title.ToLower().Contains("chill") ? 0 : 1);

                    if (id == -1)
                        await context.Events.AddAsync(eve);
                    else
                        context.Events.Update(eve);

                await context.SaveChangesAsync();
                return Json(new { status = "success" });
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
                await using var context = new OverstagContext();
                var e = await context.Events.FirstOrDefaultAsync(f => f.Id == id);
                context.Events.Remove(e);
                await context.SaveChangesAsync();
                return Json(new { status = "success" });
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
        public async Task<IActionResult> Votes()
        {
            await using var context = new OverstagContext();
            return View(await context.Ideas
                .Include(f => f.Votes)
                .OrderByDescending(b => (b.Votes.Count(i => i.Upvote) - b.Votes.Count(i => !i.Upvote)))
                .ToListAsync());
        }

        /// <summary>
        /// Delete an idea
        /// </summary>
        /// <param name="id">The idea its id</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpGet("Mentor/deleteVote/{id}")]
        public async Task<IActionResult> deleteVote(int id)
        {
            await using var context = new OverstagContext();
            try
            {
                var idea = await context.Ideas.FirstAsync(i => i.Id == id);
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
