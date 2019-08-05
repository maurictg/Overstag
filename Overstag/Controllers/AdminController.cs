using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models.NoDB;
using Overstag.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() { return View(); }
        public IActionResult Database() { return View(); }

        /// <summary>
        /// Checks if the current user is the admin
        /// </summary>
        /// <returns>true or false</returns>
        private bool CheckforAdmin()
        {
            string token = HttpContext.Session.GetString("Token");
            using (var context = new OverstagContext())
            {
                var admin = context.Accounts.First(a => a.Username.Equals("admin", StringComparison.CurrentCultureIgnoreCase));
                return ((admin.Token == token) ? true : false);
            }
        }
        /// <summary>
        /// Creates the database if not exist
        /// </summary>
        /// <returns>string with info</returns>
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
        /// <summary>
        /// Deletes the full database
        /// </summary>
        /// <returns>string with info</returns>
        public string DeleteDB()
        {
            if (CheckforAdmin())
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
            else
            {
                return "You've no permission to call this function";
            }
            
        }

        /// <summary>
        /// List all events
        /// </summary>
        /// <returns>List(Event)</returns>
        public IActionResult Events()
        {
            using(var context = new OverstagContext())
            {
                List<Event> events = context.Events.OrderBy(e => e.When).ToList();
                return View(events);
            }
        }

        /// <summary>
        /// Adds an event to the database
        /// </summary>
        /// <param name="e">A new event</param>
        /// <returns>Json result, status = "error" or status = "success"</returns>
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
                         a.Title = e.Title; a.Description = e.Description; a.When = e.When; a.Cost = e.Cost; 
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

        /// <summary>
        /// Deletes an event from the database
        /// </summary>
        /// <param name="Id">the EventID</param>
        /// <returns>Json result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> deleteEvent(int Id)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    Event eve = context.Events.Where(e => e.Id.Equals(Id)).FirstOrDefault();
                    //delete participators
                    try
                    {
                        var partici = context.Participate.Where(p => p.EventId == eve.Id).ToList();
                        foreach (var p in partici) { context.Remove(p); }
                    }
                    catch { }
                    //Delete event
                    context.Remove(eve);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", error = ex.ToString() });
                }
            }
        }

        /// <summary>
        /// List all users in the database
        /// </summary>
        /// <returns>List(Account)</returns>
        public IActionResult Users()
        {
            using (var context = new OverstagContext()){ return View(context.Accounts.ToList()); }
        }

        /// <summary>
        /// GET all participators on an event including payment info, first and last name
        /// </summary>
        /// <returns>Dictionary(Event, Dictonary(Account,bool)) with the event, the user and the bool payed</Account></returns>
        public IActionResult Participators()
        {
            List<AParticipator> aPart = new List<AParticipator>();
            using(var context = new OverstagContext())
            {
                var events = context.Events.OrderBy(e => e.When).ToArray();
                var participators = context.Participate.ToArray();

                foreach(var e in events)
                {
                    List<Account> Users = new List<Account>();
                    List<bool> Factured = new List<bool>();
                    var parti = participators.Where(o => o.EventId == e.Id);

                    foreach(var p in parti)
                    {
                        Users.Add(context.Accounts.First(u => u.Id == p.UserId));
                        Factured.Add(p.Payed==1);
                    }

                    aPart.Add(new AParticipator()
                    {
                        Accounts = Users.ToArray(),
                        Factured = Factured.ToArray(),
                        Event = e
                    });
                }
            }
            return View(aPart.OrderBy(e => e.Event.When).ToList());
        }

        public IActionResult Billing()
        {
            List<AUnpayed> aus = new List<AUnpayed>();

            using (var context = new OverstagContext())
            {
                foreach (var user in context.Accounts)
                {
                    List<Event> events = new List<Event>();
                    var parti = context.Participate.Where(p => p.UserId == user.Id);
                    foreach (var part in parti)
                    {
                        var eve = context.Events.First(e => e.Id == part.EventId);
                        if (part.Payed == 0)
                        {
                            if (Core.General.DateIsPassed(eve.When))
                                events.Add(eve);
                        }
                    }

                    aus.Add(new AUnpayed
                    {
                        User = user,
                        Unfactured_Events = events,
                        Unpayed_Invoices = context.Invoices.Where(f => f.UserID==user.Id&&f.Payed==0).ToList()
                    });
                }
            }
            

            return View(aus);
        }

        public IActionResult GenerateInvoices()
        {
            //En toen stond <3 Peet <3 opeens voor de deur :D
            return null;
            //Maak facturen als gebruiker dat nog niet zelf heeft gedaan.
            //Als er een factuur bestaat, moet bool payed = true zijn bij avond, en false bij factuur
            //Knopje bij openstaande facturen "genereren" zowel bij admin als bij user
        }

        /// <summary>
        /// Logs in as the user with that token
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        [Route("Admin/Loginas/{token}")]
        public IActionResult LoginAs(string token)
        {
            using(var context = new OverstagContext())
            {
               var usr = context.Accounts.First(a => a.Token == Uri.UnescapeDataString(token));
               HttpContext.Session.SetString("Token", usr.Token);
               HttpContext.Session.SetString("Name", usr.Username);

               Response.Redirect("/Home");
               return Content("OK");
            }
        }



        /// <summary>
        /// fire SQL query's on the database. Must be an admin of course
        /// </summary>
        /// <param name="q">Query string</param>
        /// <returns>JSON result, status = "success" including data or status = "error" including error</returns>
        [HttpPost]
        public async Task<IActionResult> Query(SQLQuery q)
        {
            if (CheckforAdmin())
            {
                if (q.Query != string.Empty)
                {
                    using (var context = new OverstagContext())
                    {
                        if (q.Type == 0) //fire
                        {
                            try
                            {
                                int rows = 0;
                                rows = await context.Database.ExecuteSqlCommandAsync(q.Query); return Json(new { status = "success", data = rows.ToString()+" rows affected"});
                            }
                            catch (Exception e) { return Json(new { status = "error", error = e.ToString() }); }
                        }
                        else if (q.Type == 1) //read single
                        {
                            try
                            {
                                switch (q.TableName)
                                {
                                    default:
                                        return Json(new {status = "error", error = "Geen table name gegeven" });
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).FirstOrDefault() });
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).FirstOrDefault() });
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).FirstOrDefault() });
                                    case "Invoices":
                                        return Json(new { status = "success", data = context.Invoices.FromSql(q.Query).FirstOrDefault() });
                                    case "Logging":
                                        return Json(new { status = "success", data = context.Logging.FromSql(q.Query).FirstOrDefault() });

                                }
                            }
                            catch(Exception e)
                            {
                                return Json(new { status = "error", error = e.ToString() });
                            }
                            
                        }
                        else if (q.Type == 2) //read object as list
                        {
                            try
                            {
                                switch (q.TableName)
                                {
                                    default:
                                        return Json(new { status = "error", error = "Geen table name gegeven" });
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).ToList() });
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).ToList() });
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToList() });
                                    case "Invoices":
                                        return Json(new { status = "success", data = context.Invoices.FromSql(q.Query).ToList() });
                                    case "Logging":
                                        return Json(new { status = "success", data = context.Logging.FromSql(q.Query).ToList() });
                                }
                            }
                            catch(Exception e)
                            {
                                return Json(new { status = "error", error = e.ToString() });
                            }
                        }
                        else if (q.Type == 3) //read object as array
                        {
                            try
                            {
                                switch (q.TableName)
                                {
                                    default:
                                        return Json(new { status = "error", error = "Geen table name gegeven" });
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).ToArray() });
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).ToArray() });
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToArray() });
                                    case "Invoices":
                                        return Json(new { status = "success", data = context.Invoices.FromSql(q.Query).ToArray() });
                                    case "Logging":
                                        return Json(new { status = "success", data = context.Logging.FromSql(q.Query).ToArray() });
                                }
                            }
                            catch (Exception e)
                            {
                                return Json(new { status = "error", error = e.ToString() });
                            }
                        }
                        else
                        {
                            return Json(new { status = "error", error = "Onjuist type opgegeven" });
                        }
                    }
                }
                else
                {
                    return Json(new { status = "error", error = "Query is NULL" });
                }

            }
            else
            {
                return Json(new { status = "error", error = "U heeft onvoldoende rechten om SQL-Query's uit te voeren" });
            }
        }
    }
}