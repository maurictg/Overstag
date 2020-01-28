using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.NoDB;
using Mollie.Api.Client;

namespace Overstag.Controllers
{
    public class UserController : Controller
    {
        private Account currentUser = null;

        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns>All account info of the current user</returns>
        public Account currentuser()
        {
            if (currentUser == null)
                currentUser = new OverstagContext().Accounts.First(f => f.Token == HttpContext.Session.GetString("Token"));

            return currentUser;
        }

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
        /// Get saved logins
        /// </summary>
        /// <returns>Logins object</returns>
        public IActionResult getLogins()
            => Json(new OverstagContext().Auths.Where(f => f.UserId == currentuser().Id).ToList());

        /// <summary>
        /// Remove login by its id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpGet("User/removeLogin/{id}")]
        public async Task<IActionResult> removeLogin(int id)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var login = context.Auths.Find(id);
                    context.Auths.Remove(login);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Verwijderen mislukt door interne fout", debuginfo = e.ToString() });
                }
            }
        }

        /// <summary>
        /// Get events page with all subscriptions
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Events()
        {
            using (var context = new OverstagContext())
            {
                var user = context.Accounts.Include(a => a.Subscriptions).First(p => p.Id == currentuser().Id);
                var parti = user.Subscriptions.ToList();

                List<ISubscription> events = new List<ISubscription>();

                foreach (var eve in context.Events.Where(e => e.When.Date >= DateTime.Today).OrderBy(e => e.When).ToList())
                {
                    bool hasparti = parti.Any(f => f.EventID == eve.Id);
                    events.Add(new ISubscription()
                    {
                        Event = eve,
                        Subscribed = hasparti,
                        Friends = (hasparti ? parti.First(f => f.EventID == eve.Id).FriendCount : 0)
                    });
                }

                return View(events);
            }
        }

        /// <summary>
        /// Get all unpayed events and invoices
        /// </summary>
        /// <returns>View with invoices and unpayed events</returns>
        public IActionResult Payment()
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.Include(f => f.Subscriptions).Include(f => f.Family).First(f => f.Id == currentuser().Id);
                ViewBag.Age = Core.General.getAge(user.Birthdate);

                List<Event> events = context.Events.ToList();
                List<Event> unpayedEvents = new List<Event>();
                List<Invoice> invoices = context.Invoices.Include(g => g.User).ThenInclude(h => h.Subscriptions).Where(f => f.User.Id == user.Id).ToList();
                List<XInvoice> iinvoices = new List<XInvoice>();

                foreach(var p in user.Subscriptions)
                {
                    if(!p.Payed)
                    {
                        var e = events.First(f => f.Id == p.EventID);
                        if (!unpayedEvents.Contains(e) && Core.General.DateIsPassed(e.When))
                            unpayedEvents.Add(e);
                    }
                }

                if(invoices.Count() > 0)
                {
                    foreach(var i in invoices)
                        iinvoices.Add(Services.Invoices.GetXInvoice(i));
                }

                ViewBag.HasFamily = (user.Family != null);
                ViewBag.IsParent = (user.Type == 1);

                return View("Payment", new UnpayedEvents()
                {
                    Invoices = iinvoices,
                    Subscriptions = user.Subscriptions,
                    UnfacturedEvents = unpayedEvents.OrderBy(f => f.When).ToList()
                });
            }
        }


        /// <summary>
        /// Get all subscribers of an event
        /// </summary>
        /// <param name="eventID">The event's id</param>
        /// <returns>JSON serialized</returns>
        [Route("User/getSubscribers/{eventID}")]
        public JsonResult getSubscribers(int eventID)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    List<int> subs = context.Events.Include(f => f.Participators)
                        .First(g => g.Id == eventID).Participators.Select(f => f.UserID).ToList();
                    subs.Remove(currentuser().Id);
                    List<string> Users = context.Accounts.Where(f => subs.Contains(f.Id)).Select(f => $"{f.Firstname} {f.Lastname}").ToList();
                    return Json(new { status = "success", data = Users.ToArray() });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", debuginfo = e.Message });
                }
            }
        }

        /// <summary>
        /// Renders all ideas with up and downvotes from user
        /// </summary>
        /// <returns>A View with votes</returns>
        public IActionResult Ideas()
        {
            ViewBag.UserID = currentuser().Id;
            return View(new OverstagContext().Ideas.Include(f => f.Votes).OrderBy(b => (b.Votes.Count(i => i.Upvote)- b.Votes.Count(i => !i.Upvote))).ToArray().Reverse().ToList());
        }

        public IActionResult Vote() 
            => View();

        public IActionResult Declaration()
            => View(new OverstagContext().Transactions.Include(x => x.User).Where(f => f.User.Id == currentuser().Id && f.Type == 29).OrderByDescending(g => g.Timestamp).ToList());

        /// <summary>
        /// Post a new idea to the server
        /// </summary>
        /// <param name="idea">The formencoded Idea</param>
        /// <returns>JSON (status = "success" or "error")</returns>
        [HttpPost("User/Vote/postIdea")]
        public async Task<IActionResult> postIdea(Idea idea)
        {
            if (currentuser().Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet een idee indienen." });

            try
            {
                using (var context = new OverstagContext())
                {
                    context.Ideas.Add(idea);
                    await context.SaveChangesAsync();
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
        public async Task<IActionResult> Like(int id, byte like)
        {
            if (currentuser().Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet stemmen voor een activiteit." });

            using (var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Votes).First(u => u.Id == currentuser().Id);

                    if (user.Votes.Any(u => u.IdeaID == id))
                        user.Votes.First(u => u.IdeaID == id).Upvote = (like==1);
                    else
                    {
                        user.Votes.Add(new Models.Vote
                        {
                            IdeaID = id,
                            UserID = user.Id,
                            Upvote = (like==1)
                        });
                    }
                    context.Accounts.Update(user);
                    await context.SaveChangesAsync();
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

                    if (DateTime.Today > eve.When.Date)
                        return Json(new { status = "error", error = "Deze activiteit is al voorbij. U kunt zich hiervoor niet meer inschrijven" });

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
                            return Json(new { status = "error", error = "Je bent al uitgeschreven voor deze activiteit" });
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
        /// Increase or decrease FriendCount in subscription model
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <param name="incr">The incrementer</param>
        /// <returns>JSON result, status=error or status=success</returns>
        [HttpPost]
        public async Task<IActionResult> SubscribeFriends([FromForm]int id, [FromForm]int amount)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Subscriptions).First(f => f.Id == currentuser().Id);
                    var part = user.Subscriptions.First(f => f.EventID == id);
                    part.FriendCount = (byte)((amount>0) ? amount : 0);
                    context.Accounts.Update(user);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = e.Message });
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> postDeclaration(Accountancy.Transaction request)
        {
            try
            {
                request.Timestamp = DateTime.Now;
                request.Payed = false;
                request.UserId = currentuser().Id;
                request.Type = 29; //Declaratie
                using (var context = new OverstagContext())
                {
                    context.Transactions.Add(request);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "success", error = "Interne fout", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Changes the password of the user
        /// </summary>
        /// <param name="p">Old pass and new pass</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postPasswordChange([FromForm]string Oldpass, [FromForm]string Newpass)
        {
            using (var context = new OverstagContext())
            {
                var cuser = currentuser();
                if (Encryption.PBKDF2.Verify(cuser.Password, Oldpass))
                {
                    try
                    {
                        cuser.Password = Encryption.PBKDF2.Hash(Newpass);
                        cuser.Token = Encryption.Random.rHash(Encryption.SHA.S256(cuser.Firstname) + cuser.Username);
                        context.Accounts.Update(cuser);
                        await context.SaveChangesAsync();
                        HttpContext.Session.SetString("Token", cuser.Token);

                        var mail = new Services.PassWarningMail(cuser);
                        if (!mail.SendAsync().Result)
                            return Json(new { status = "warning", error = "Mail verzonden mislukt", debuginfo = mail.error.ToString() });
                        else
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
                var cuser = context.Accounts.First(f => f.Id == currentuser().Id);
                cuser.Firstname = a.Firstname;
                cuser.Lastname = a.Lastname;
                cuser.Adress = a.Adress;
                cuser.Residence = a.Residence;
                cuser.Postalcode = a.Postalcode;
                cuser.Phone = a.Phone;

                //Check if email is in use
                var x = context.Accounts.FirstOrDefault(f => f.Email == a.Email);
                if (x != null && x.Token != cuser.Token)
                    return Json(new { status = "error", error = "Emailadres is al in gebruik!" });
                else
                    cuser.Email = a.Email;

                //Check if username is in use
                var y = context.Accounts.FirstOrDefault(f => f.Username == a.Username);
                if (y != null && y.Token != cuser.Token)
                    return Json(new { status = "error", error = "Gebruikersnaam is al in gebruik!" });
                else
                    cuser.Username = a.Username;

                try
                {
                    context.Accounts.Update(cuser);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden.", debuginfo = e.ToString() });
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
                    var account = context.Accounts.Include(f => f.Auths).Include(g => g.Invoices).Include(h => h.Payments).Include(i => i.Subscriptions).Include(j => j.Votes).Include(x => x.Transactions).First(k => k.Token == Uri.UnescapeDataString(a.Token));

                    if (currentuser().Username != "admin" && currentuser().Username != account.Username)
                        return Json(new { status = "error", error = "Mislukt door authenticatiefout!" });

                    if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                    {
                        if (account.Invoices.Count(f => !f.Payed) > 0)
                            return Json(new { status = "error", error = "U heeft nog onbetaalde facturen openstaan." });

                        if (account.Subscriptions.Count(f => !f.Payed) > 0)
                            return Json(new { status = "error", error = "U heeft nog ongefactureerde activiteiten openstaan." });

                        try
                        {
                            //All data is removed thanks to cascade and the includes
                            //Remove mollie integration
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

                            //Remove account
                            context.Accounts.Remove(account);
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
        public async Task<JsonResult> GenerateInvoice()
        {
            bool result = await Services.Invoices.Create(currentuser().Id);
            if(result)
                return Json(new { status = "success" });
            else
                return Json(new { status = "error", error = Services.Invoices.error.ToString() });
        }

        public async Task<JsonResult> MergeInvoices()
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.Include(f => f.Invoices).First(f => f.Id == currentuser().Id);
                var invoices = user.Invoices.Where(f => !f.Payed).ToList();
                if (invoices.Count() > 1)
                {
                    Invoice i = new Invoice();

                    int additions = 0;
                    int bill = 0;
                    List<string> EventIDS = new List<string>();
                    
                    foreach(var invoice in invoices)
                    {
                        additions += invoice.AdditionsCost;
                        bill += invoice.Amount;
                        i.InvoiceID = invoice.InvoiceID;
                        EventIDS.AddRange(invoice.EventIDs.Split(',').ToList());
                        user.Invoices.Remove(invoice);
                    }

                    i.EventIDs = string.Join(',', EventIDS);
                    i.Amount = bill;
                    i.AdditionsCost = additions;
                    i.Payed = false;
                    i.Timestamp = DateTime.Now;

                    try
                    {
                        user.Invoices.Add(i);
                        context.Accounts.Update(user);
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                    catch(Exception e)
                    {
                        return Json(new { status = "error", error = "Er is iets fout gegaan", debuginfo = e.ToString() });
                    }
                }
                else
                {
                    return Json(new { status = "error", error = "Om samen te voegen heb je meer dan 1 openstaande factuur nodig" });
                }
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