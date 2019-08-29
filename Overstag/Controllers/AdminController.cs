using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models.NoDB;
using Overstag.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Client;
using Mollie.Api.Models.List;
using Mollie.Api.Models.Payment.Response;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index() 
            => View();

        public IActionResult Database() 
            => View();

        /// <summary>
        /// Checks if the current user is the admin
        /// </summary>
        /// <returns>true or false</returns>
        private bool CheckforAdmin()
        {
            using (var context = new OverstagContext())
                return (HttpContext.Session.GetInt32("Type") != null && HttpContext.Session.GetInt32("Type") == 3);
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
        /// Get and return all payments
        /// </summary>
        /// <returns>View with mollie payments</returns>
        public async Task<IActionResult> PayingMollie()
        {
            IPaymentClient paymentClient = new PaymentClient(Core.General.Credentials.mollieApiToken);
            ListResponse<PaymentResponse> response = await paymentClient.GetPaymentListAsync();
            return View(response.Items);
        }

        /// <summary>
        /// Get all payments from database
        /// </summary>
        /// <returns>View with database payments</returns>
        public IActionResult Paying()
            => View(new OverstagContext().Payments.OrderBy(f => f.PlacedAt).ToArray().Reverse().ToArray());

        
        /// <summary>
        /// Returns the data of an event by it's id
        /// </summary>
        /// <param name="id">The eventID</param>
        /// <returns>Json with data: serialized event</returns>
        [Route("Admin/getEvent/{id}")]
        public IActionResult getEvent(int id)
        {
            using (var context = new OverstagContext())
            {
                var eve = context.Events.First(e => e.Id == id);
                if (eve != null)
                    return Json(new { status = "success", data = eve });
                else
                    return Json(new { status = "error", error = "Event bestaat niet" });
            }
        }

        /// <summary>
        /// Updates an existing event
        /// </summary>
        /// <param name="e">The event</param>
        /// <returns>Json, success or error</returns>
        [HttpPost]
        public IActionResult UpdateEvent(Event e)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var eve = context.Events.First(f => f.Id == e.Id);
                    eve.Title = e.Title;
                    eve.Description = e.Description;
                    eve.Cost = e.Cost;
                    eve.When = e.When;
                    context.Events.Update(eve);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch(Exception ex)
                {
                    return Json(new { status = "error", error = ex.Message });
                }
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
                    Event eve = context.Events.Where(e => e.Id.Equals(Id)).Include(f => f.Participators).FirstOrDefault();
                    //delete participators
                    try
                    {
                        var partici = eve.Participators;
                        foreach (var p in partici) { eve.Participators.Remove(p); }
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
        /// Get all ideas sorted by votes
        /// </summary>
        /// <returns>View with sorted ideas</returns>
        public IActionResult Ideas()
            => View(new OverstagContext().Ideas.Include(f => f.Votes).OrderBy(b => (b.Votes.Count(i => i.Upvote == 1) - b.Votes.Count(i => i.Upvote == 0))).ToArray().Reverse().ToList());
        
        /// <summary>
        /// Delete an idea
        /// </summary>
        /// <param name="id">The ID of the idea</param>
        /// <returns>JSON (status = success or status = error with details)</returns>
        [HttpGet("Admin/Ideas/Delete/{id}")]
        public IActionResult DeleteIdea(int id)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var idea = context.Ideas.First(i => i.Id == id);
                    context.Ideas.Remove(idea);
                    context.SaveChanges();
                    return Json(new { status = "success"});
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = e });
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
                var events = context.Events.Include(f => f.Participators).OrderBy(e => e.When).ToArray();
                //var participators = context.Participate.ToArray();

                foreach(var e in events)
                {
                    List<Account> Users = new List<Account>();
                    List<bool> Factured = new List<bool>();
                    var parti = e.Participators;

                    if(parti != null)
                    {
                        foreach (var p in parti)
                        {
                            Users.Add(context.Accounts.First(u => u.Id == p.UserID));
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
            }
            return View(aPart.OrderBy(e => e.Event.When).ToList());
        }

        /// <summary>
        /// Get an overview of all unfactured events and unpayed invoices for all users
        /// </summary>
        /// <returns>View with a List of AUnpayed objects</returns>
        public IActionResult Billing()
        {
            List<AUnpayed> aus = new List<AUnpayed>();

            using (var context = new OverstagContext())
            {
                foreach (var user in context.Accounts.Include(f => f.Subscriptions))
                {
                    List<Event> events = new List<Event>();
                    var parti = user.Subscriptions;

                    if(parti != null)
                    {
                        foreach (var part in parti)
                        {
                            var eve = context.Events.Include(f => f.Participators).First(e => e.Id == part.EventID);
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
                            Unpayed_Invoices = context.Invoices.Where(f => f.UserID == user.Id && f.Payed == 0).ToList()
                        });
                    }
                    
                }
            }
            
            return View(aus);
        }

        /*//NOT IN USE 
        public IActionResult GenerateInvoices()
        {
            //En toen stond <3 Peet <3 opeens voor de deur :D
            return null;
            //Maak facturen als gebruiker dat nog niet zelf heeft gedaan.
            //Als er een factuur bestaat, moet bool payed = true zijn bij avond, en false bij factuur
            //Knopje bij openstaande facturen "genereren" zowel bij admin als bij user
        }*/

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
               HttpContext.Session.SetInt32("Type", usr.Type);
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
                                    //case "Participate":
                                    //    return Json(new { status = "success", data = context.Participate.FromSql(q.Query).FirstOrDefault() });
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
                                    //case "Participate":
                                    //    return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToList() });
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
                                    //case "Participate":
                                    //    return Json(new { status = "success", data = context.Participate.FromSql(q.Query).ToArray() });
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