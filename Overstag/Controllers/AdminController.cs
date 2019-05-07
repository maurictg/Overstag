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
        public IActionResult Index()
        {
            return View();
        }

        private bool CheckforAdmin()
        {
            string token = HttpContext.Session.GetString("Token");
            using (var context = new OverstagContext())
            {
                var admin = context.Accounts.First(a => a.Username.Equals("admin", StringComparison.CurrentCultureIgnoreCase));
                return ((admin.Token == token) ? true : false);
            }
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

        public IActionResult Users()
        {
            using (var context = new OverstagContext())
            {
                return View(context.Accounts.ToList());
            }
        }

        public IActionResult Participators()
        {
            Dictionary<Event, Dictionary<Account,bool>> deelnemers = new Dictionary<Event, Dictionary<Account,bool>>();
            using (var context = new OverstagContext())
            {
                var participators = context.Participate.ToList();
                var events = context.Events.ToList();

                foreach(var eve in events)
                {
                    var part = participators.Where(p => p.EventId == eve.Id);
                    //Add users
                    Dictionary<Account, bool> users = new Dictionary<Account, bool>();
                    foreach(var p in part)
                    {
                        var account = context.Accounts.First(u => u.Id == p.UserId);
                        users.Add(account,(p.Payed != 0));
                    }

                    deelnemers.Add(eve, users);
                }
            }
            return View(deelnemers);
        }

        public IActionResult Database()
        {
            return View();
        }

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
                                        break;
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).FirstOrDefault() });
                                        break;
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).FirstOrDefault() });
                                        break;
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).FirstOrDefault() });
                                        break;
                                    
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
                                        break;
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).ToList() });
                                        break;
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).ToList() });
                                        break;
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToList() });
                                        break;
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
                                        break;
                                    case "Accounts":
                                        return Json(new { status = "success", data = context.Accounts.FromSql(q.Query).ToArray() });
                                        break;
                                    case "Events":
                                        return Json(new { status = "success", data = context.Events.FromSql(q.Query).ToArray() });
                                        break;
                                    case "Participate":
                                        return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToArray() });
                                        break;
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