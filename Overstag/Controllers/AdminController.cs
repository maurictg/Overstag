using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                List<Event> events = context.Events.OrderBy(e => e.Date).ToList();
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
            //ERROR!!!
            try
            {
                Dictionary<Event, Dictionary<Account, bool>> deelnemers = new Dictionary<Event, Dictionary<Account, bool>>();
                using (var context = new OverstagContext())
                {
                    var participators = context.Participate.ToList();
                    var events = context.Events.OrderBy(e => e.Date).ToList();

                    foreach (var eve in events)
                    {
                        var part = participators.Where(p => p.EventId == eve.Id);
                        //Add users
                        Dictionary<Account, bool> users = new Dictionary<Account, bool>();
                        foreach (var p in part)
                        {
                            var account = context.Accounts.First(u => u.Id == p.UserId);
                            users.Add(account, (p.Payed != 0));
                        }

                        deelnemers.Add(eve, users);
                    }
                }
                return View(deelnemers);
            }
            catch(Exception e)
            {
                return Content(e.ToString());
            }
        }

        public IActionResult Billing()
        {
            Dictionary<Account, List<Event>> unpayed = new Dictionary<Account, List<Event>>();
            using(var context = new OverstagContext())
            {
                
                var a = context.Accounts.ToList();
                foreach(var e in a)
                {
                    List<Event> events = new List<Event>();
                    var u = context.Participate.Where(b => b.UserId == e.Id).Where(p => p.Payed == 0).ToList();
                    foreach(var i in u)
                    {
                        events.Add(context.Events.First(f => f.Id == i.EventId));
                    }
                    unpayed.Add(e, events);
                }
            }

            return View(unpayed);
        }

        public IActionResult GenerateIvoices()
        {
            //En toen stond <3 Peet <3 opeens voor de deur :D
            return null;
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