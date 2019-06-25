using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;


namespace Overstag.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns>All account info of the current user</returns>
        public Account currentuser() { using (var c = new OverstagContext()) { try { return c.Accounts.Where(a => a.Token.Equals(HttpContext.Session.GetString("Token"))).FirstOrDefault(); } catch { return null; } } }

        public IActionResult Index() {return View(currentuser());}

        public IActionResult Settings(){return View(currentuser());}
        public IActionResult Events() { return View(); }
        public IActionResult Payment() {

            using (var context = new OverstagContext())
            {
                var parti = context.Participate.Where(p => p.UserId == currentuser().Id).ToList();
                List<Event> events = new List<Event>();

                foreach (var part in parti)
                {
                    if (!events.Contains(context.Events.Where(e => e.Id == part.EventId).FirstOrDefault()) && part != null)
                    {
                        if (part.Payed == 0)
                        {
                            var e2 = context.Events.Where(e => e.Id == part.EventId).FirstOrDefault();
                            if(Core.General.DateIsPassed(e2.When))
                                events.Add(e2);
                        }
                    }
                }

                var invoices = context.Invoices.Where(i => i.UserID == currentuser().Id).OrderBy(v => v.Payed);
                List<Models.NoDB.IInvoice> Inv = new List<Models.NoDB.IInvoice>();

                foreach(var i in invoices)
                {
                    List<Event> events1 = new List<Event>();

                    foreach (var e in i.EventIDs.Split(','))
                        events1.Add(context.Events.First(j => j.Id == Convert.ToInt32(e)));

                    Inv.Add(new Models.NoDB.IInvoice
                    {
                        Payed = (i.Payed == 1),
                        Amount = i.Amount,
                        Events = events1,
                        Timestamp = i.Timestamp,
                        UserID = i.UserID,
                        PayID = i.PayID
                    });
                }

                var data = new Overstag.Models.NoDB.UnpayedEvents
                {
                    UnfacturedEvents = events = events.OrderBy(e => e.When).ToList(),
                    Invoices = Inv
                };

                return View(data);
            }
        }


        /// <summary>
        /// A partial view rendered into other views
        /// </summary>
        /// <returns>A view with events</returns>
        public IActionResult Subscriptions()
        {
            using (var context = new OverstagContext())
            {
                var parti = context.Participate.Where(p => p.UserId == currentuser().Id).ToList();
                List<Event> events = new List<Event>();
                events.Clear();

                foreach(var part in parti)
                {
                    if(part != null)
                    {
                        events.Add(context.Events.First(e => e.Id == part.EventId));
                    }
                }
                return View(new Overstag.Models.NoDB.Subscriptions() { Events = events.Where(e=>e.When>DateTime.Now).OrderBy(e => e.When).ToList() });
            }
        }

       

        /// <summary>
        /// Adds an subscription to the user for an upcoming event
        /// </summary>
        /// <param name="p">The EventID</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postSubscribeEvent(Participate p)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.First(a => a.Id == currentuser().Id);
                    var eve = context.Events.First(e => e.Id == p.EventId);
                    var part = context.Participate.Where(e => e.EventId == p.EventId && e.UserId == user.Id).FirstOrDefault();
                    if (part == null)
                    {
                        context.Participate.Add(new Participate { UserId = user.Id, EventId = eve.Id, Payed = 0 });
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    else
                    {
                        return Json(new { status = "error", error = "Je bent al ingeschreven voor deze activiteit!" });
                    }
                    
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                }

            }
        }

        /// <summary>
        /// Deletes an subscription from a user
        /// </summary>
        /// <param name="p">The event ID</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postDeleteEvent(Participate p)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.First(a => a.Id == currentuser().Id);
                    var eve = context.Events.First(e => e.Id == p.EventId);
                    try
                    {
                        var part = context.Participate.Where(e => e.EventId == p.EventId && e.UserId == user.Id).FirstOrDefault();
                        if (part == null)
                        {
                            return Json(new { status = "error", error = "U bent al uitgeschreven voor deze activiteit" });
                        }
                        else
                        {
                            context.Participate.Remove(part);
                            await context.SaveChangesAsync();
                            return Json(new { status = "success" });
                        }
                    }
                    catch(Exception e)
                    {
                        return Json(new { status = "error", error = "Er is een error opgetreden: NULL", debuginfo = e.ToString() });
                    }


                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                }

            }
        }

        /// <summary>
        /// Changes the password of the user
        /// </summary>
        /// <param name="p">Old pass and new pass</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postPasswordChange(Password p)
        {
            using (var context = new OverstagContext())
            {
                var cuser = currentuser();
                if (Encryption.PBKDF2.Verify(cuser.Password, p.Oldpass))
                {
                    try
                    {
                        cuser.Password = Encryption.PBKDF2.Hash(p.Newpass);
                        cuser.Token = Encryption.Random.rHash(Encryption.SHA.S256(cuser.Firstname) + cuser.Username);
                        context.Update(cuser);
                        await context.SaveChangesAsync();
                        HttpContext.Session.SetString("Token", cuser.Token);
                        return Json(new { status = "success" });
                    }
                    catch(Exception e)
                    {
                        return Json(new { status = "error", error = "Er is een interne fout opgetreden.",debuginfo = e.ToString() });
                    }
                }
                else
                {
                    return Json(new { status = "error", error = "Huidig wachtwoord onjuist" });
                }
            }

        }

        /// <summary>
        /// Changes the account info of an user
        /// </summary>
        /// <param name="a">Updated account info</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postInfoChange(Account a)
        {
            using (var context = new OverstagContext())
            {
                var cuser = context.Accounts.Where(x => x.Token == currentuser().Token).FirstOrDefault();
                cuser.Firstname = a.Firstname;
                cuser.Lastname = a.Lastname;
                cuser.Adress = a.Adress;
                cuser.Residence = a.Residence;
                cuser.Postalcode = a.Postalcode;
                //Check if email is in use
                bool emailinuse = false;
                try
                {
                    var testem = context.Accounts.Where(b => b.Email == a.Email).FirstOrDefault();
                    if(testem.Token != cuser.Token && testem.Email == a.Email) //Dan is er dus een emailadres opgegeven dat van iemand anders is
                    {
                        emailinuse = true;
                    }
                }
                catch { emailinuse = false; }

                if (emailinuse)
                {
                    return Json(new { status = "error", error = "Emailadres is al in gebruik!" });
                }
                else
                {
                    cuser.Email = a.Email;
                    try
                    {
                        context.Update(cuser);
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { status = "error", error = "Er is een interne fout opgetreden.", debuginfo = e.ToString() });
                    }
                }
                
            }

        }

        /// <summary>
        /// Deletes all info about an user in the database and removes the account
        /// </summary>
        /// <param name="a">The account</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postDeleteAccount(Account a)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var account = context.Accounts.Where(e => e.Token == a.Token).FirstOrDefault();

                    if (currentuser().Username != "admin" && currentuser().Username != account.Username)
                        return Json(new { status = "error", error = "Mislukt door authenticatiefout!" });

                    if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                    {
                        if (context.Invoices.Where(i => i.UserID == account.Id && i.Payed == 0).Count() > 0)
                            return Json(new { status = "error", error = "U heeft nog onbetaalde facturen openstaan." });

                        try
                        {
                            //Ingeschreven events verwijderen
                            try
                            {
                                var subscriptions = context.Participate.Where(p => p.UserId == account.Id).ToList();
                                foreach (var item in subscriptions)
                                {
                                    context.Remove(item);
                                }
                            }
                            catch { }

                            //Account verwijderen
                            context.Remove(account);
                            await context.SaveChangesAsync();
                            return Json(new { status = "success" });
                        }
                        catch (Exception e)
                        {
                            return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                        }
                    }
                    else { return Json(new { status = "error", error = "Token of wachtwoord onjuist" }); }
                }
                catch { return Json(new { status = "error", error = "Token bestaat niet" }); }
            }
        }

        [HttpGet]
        public JsonResult GenerateInvoice()
        {
            using (var context = new OverstagContext())
            {
                var parti = context.Participate.Where(i => i.UserId == currentuser().Id && i.Payed == 0);

                if (parti.Count() < 1)
                    return Json(new { status = "error", error = "Geen openstaande events gevonden" });

                List<int> eventIDS = new List<int>();

                int bill = 0;

                try {

                    foreach (var part in parti)
                    {
                        var eve = context.Events.First(f => f.Id == part.EventId);
                        if (Core.General.DateIsPassed(eve.When))
                        {
                            part.Payed = 1;
                            bill += eve.Cost;
                            eventIDS.Add(eve.Id);
                        }
                    }

                    var facture = new Invoice()
                    {
                        UserID = currentuser().Id,
                        Amount = bill,
                        EventIDs = string.Join(',', eventIDS),
                        Payed = 0,
                        Timestamp = DateTime.Now,
                        PayID = Encryption.Random.rHash(currentuser().Token)
                    };

                    context.Invoices.Add(facture);
                    context.Participate.UpdateRange(parti);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch (Exception e) { return Json(new { status = "error", error = "Mislukt door interne fout", debuginfo = e.Message }); }

            }
        }

    }
}