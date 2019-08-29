using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Mollie.Api.Client;

namespace Overstag.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns>All account info of the current user</returns>
        public Account currentuser()
            => new OverstagContext().Accounts.First(f => f.Token == HttpContext.Session.GetString("Token"));

        /// <summary>
        /// Get user homepage
        /// </summary>
        /// <returns>User homepage (view)</returns>
        public IActionResult Index() => 
            View(currentuser());

        /// <summary>
        /// Get settings with user info
        /// </summary>
        /// <returns>View with User modal including family info</returns>
        public IActionResult Settings() 
            => View(new OverstagContext().Accounts.Include(f => f.Family).First(g => g.Token == HttpContext.Session.GetString("Token")));

        /// <summary>
        /// Get events page (partials are loaded into this view)
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Events()
            =>View();

        /// <summary>
        /// Get invoices and unfactured events of user
        /// </summary>
        /// <returns>View with UnpayedEvents object including List with IInvoices and List with events</returns>
        public IActionResult Payment() {

            using (var context = new OverstagContext())
            {
                var user = context.Accounts.Include(f => f.Subscriptions).First(p => p.Id == currentuser().Id);
                var parti = user.Subscriptions;

                List<Event> events = new List<Event>();

                foreach (var part in parti)
                {
                    if (!events.Contains(context.Events.Where(e => e.Id == part.EventID).FirstOrDefault()) && part != null)
                    {
                        if(part.Payed == 0)
                        {
                            var e2 = context.Events.Where(e => e.Id == part.EventID).FirstOrDefault();
                            if (Core.General.DateIsPassed(e2.When))
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
                var user = context.Accounts.Include(a => a.Subscriptions).First(p => p.Id == currentuser().Id);
                var parti = user.Subscriptions;

                List<Event> events = new List<Event>();

                foreach(var part in parti)
                    if(part != null)
                        events.Add(context.Events.First(e => e.Id == part.EventID));

                return View(new Overstag.Models.NoDB.Subscriptions() { Events = events.Where(e=>e.When>DateTime.Now).OrderBy(e => e.When).ToList() });
            }
        }

        /// <summary>
        /// Renders all ideas with up and downvotes from user
        /// </summary>
        /// <returns>A View with votes</returns>
        public IActionResult Ideas()
        {
            ViewBag.UserID = currentuser().Id;
            return View(new OverstagContext().Ideas.Include(f => f.Votes).OrderBy(b => (b.Votes.Count(i => i.Upvote==1)- b.Votes.Count(i => i.Upvote == 0))).ToArray().Reverse().ToList());
        }

        public IActionResult Vote() => View();

        /// <summary>
        /// Post a new idea to the server
        /// </summary>
        /// <param name="idea">The formencoded Idea</param>
        /// <returns>JSON (status = "success" or "error")</returns>
        [HttpPost("User/Vote/postIdea")]
        public IActionResult postIdea(Idea idea)
        {
            if (currentuser().Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet een idee indienen." });

            try
            {
                using (var context = new OverstagContext())
                {
                    context.Ideas.Add(idea);
                    context.SaveChanges();
                }
                return Json(new { status = "success" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Mislukt vanwege interne fout", devinfo = e });
            }
        }

        /// <summary>
        /// Like an event
        /// </summary>
        /// <param name="id">The id of the idea</param>
        /// <param name="like">Upvote (1) or Downvote (0)</param>
        /// <returns>Json, status = success or error with details</returns>
        [HttpGet("User/Vote/Like/{id}/{like}")]
        public IActionResult Like(int id, byte like)
        {
            if (currentuser().Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet stemmen voor een activiteit." });

            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Votes).First(u => u.Id == currentuser().Id);

                    if (user.Votes.Any(u => u.IdeaID == id))
                        user.Votes.First(u => u.IdeaID == id).Upvote = like;
                    else
                    {
                        user.Votes.Add(new Models.Vote
                        {
                            IdeaID = id,
                            UserID = user.Id,
                            Upvote = like
                        });
                    }
                    context.Accounts.Update(user);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Mislukt vanwege interne fout", devinfo = e });
                }
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
            if (currentuser().Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u zich niet inschrijven voor een activiteit." });

            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Subscriptions).First(a => a.Id == currentuser().Id);
                    var eve = context.Events.First(e => e.Id == p.EventID);

                    if (Core.General.DateIsPassed(eve.When))
                        return Json(new { status = "error", error = "Dit event is al voorbij. U kunt zich hiervoor niet meer inschrijven" });

                    var part = user.Subscriptions.Where(e => e.EventID == p.EventID).FirstOrDefault();
                    if(part == null)
                    {
                        user.Subscriptions.Add(new Participate { UserID = user.Id, EventID = eve.Id });
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    else
                        return Json(new { status = "error", error = "Je bent al ingeschreven voor deze activiteit!" });
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
                    var user = context.Accounts.Include(f => f.Subscriptions).First(a => a.Id == currentuser().Id);
                    var eve = context.Events.First(e => e.Id == p.EventID);

                    if (Core.General.DateIsPassed(eve.When))
                        return Json(new { status = "error", error = "Dit event is al voorbij. U kunt zich hiervoor niet meer uitschrijven" });

                    try
                    {
                        var part = user.Subscriptions.Where(e => e.EventID == p.EventID).FirstOrDefault();
                        if (part == null)
                        {
                            return Json(new { status = "error", error = "U bent al uitgeschreven voor deze activiteit" });
                        }
                        else
                        {
                            user.Subscriptions.Remove(part);
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
                    var account = context.Accounts.Where(e => e.Token == Uri.UnescapeDataString(a.Token)).Include(f => f.Subscriptions).FirstOrDefault();

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
                                foreach (var item in account.Subscriptions)
                                {
                                    account.Subscriptions.Remove(item);
                                }
                            }
                            catch { }

                            //Mollie integratie verwijderen
                            try
                            {
                                if (!string.IsNullOrEmpty(account.MollieID))
                                {
                                    CustomerClient customerClient = new CustomerClient(Core.General.Credentials.mollieApiToken);
                                    await customerClient.DeleteCustomerAsync(account.MollieID);
                                }
                            }
                            catch {
                                return Json(new { status = "warning", warning = "Mollie integratie verwijderen mislukt" });
                            }

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
                catch(Exception e) { return Json(new { status = "error", error = "Token bestaat niet", debuginfo=e }); }
            }
        }

        /// <summary>
        /// Generates invoice from all unpayed events of current user
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        [HttpGet]
        public JsonResult GenerateInvoice()
        {
            using (var context = new OverstagContext())
            {
                var user = context.Accounts.Include(p => p.Subscriptions).First(f => f.Id == currentuser().Id);
                var parti = user.Subscriptions;

                if (parti.Count() < 1)
                    return Json(new { status = "error", error = "Geen openstaande events gevonden" });

                List<int> eventIDS = new List<int>();

                int bill = 0;

                try {

                    foreach (var part in parti)
                    {
                        var eve = context.Events.First(f => f.Id == part.EventID);
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

                    user.Subscriptions = parti;
                    context.Accounts.Update(user);

                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch (Exception e) { return Json(new { status = "error", error = "Mislukt door interne fout", debuginfo = e.Message }); }

            }
        }

        /// <summary>
        /// Toggles the 2FA for the current user
        /// </summary>
        /// <returns>Json (status = success with the secret or status = error)</returns>
        [HttpGet]
        public JsonResult Toggle2FA()
        {
            if (string.IsNullOrEmpty(currentuser().TwoFactor))
            {
                //aanzetten
                string secret = Security.TFA.GenerateSecret(currentuser().Token);
                return Json(new { status = "success", secret });
            }
            else
            {
                if (Security.TFA.Disable(currentuser().Token))
                    return Json(new { status = "success", secret = "" });
                else
                    return Json(new { status = "error" });
            }
        }

        /// <summary>
        /// Gets the 2fa secret of the current user
        /// </summary>
        /// <returns>Json (status = success with the secret or status = error with reason)</returns>
        [HttpGet]
        public JsonResult Get2FA()
        {
            if (!string.IsNullOrEmpty(currentuser().TwoFactor))
            {
                string secret = Security.TFA.GetSecret(currentuser().Token);
                return Json(new { status = "success", secret });
            }
            else
            {
                return Json(new { status = "error", error = "2FA is niet ingeschakeld" });
            }
        }

        /// <summary>
        /// Gets 2fa codes (beta)
        /// </summary>
        /// <returns>List with 2fa restore codes</returns>
        public JsonResult Get2FACodes()
            => Json(new { status = "success", data = Security.TFA.GetBackupCodes(currentuser().Token)});

        /// <summary>
        /// Leave the family
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        public IActionResult LeaveFamily()
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var user = context.Accounts.Include(f => f.Family).First(u => u.Id == currentuser().Id);
                    user.Family = null;
                    context.Accounts.Update(user);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "error", debuginfo = e });
            }
            
        }

    }
}