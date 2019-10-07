using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.NoDB;
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
        /// Get events page with all subscriptions
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

                if(invoices.Count() > 0)
                {
                    foreach (var i in invoices)
                    {
                        List<Event> events1 = new List<Event>();

                        foreach (string e in i.EventIDs.Split(','))
                        {
                            var eve = context.Events.FirstOrDefault(j => j.Id == Convert.ToInt32(e));
                            if(eve != null)
                                events1.Add(eve);
                        }
                                
                            
                        Inv.Add(new Models.NoDB.IInvoice
                        {
                            Payed = (i.Payed == 1),
                            Amount = i.Amount,
                            Events = events1,
                            Timestamp = i.Timestamp,
                            UserID = i.UserID,
                            PayID = i.PayID,
                            Additions = i.AdditionsCount
                        });
                    }
                }
                

                var data = new Overstag.Models.NoDB.UnpayedEvents
                {
                    UnfacturedEvents = events = events.OrderBy(e => e.When).ToList(),
                    Invoices = Inv,
                    Subscriptions = parti
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

                    if (DateTime.Today > eve.When.Date)
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
        /// Increase or decrease FriendCount in subscription model
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <param name="incr">The incrementer</param>
        /// <returns>JSON result, status=error or status=success</returns>
        [HttpPost]
        public IActionResult SubscribeFriends([FromForm]int id, [FromForm]int amount)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var user = context.Accounts.Include(f => f.Subscriptions).First(f => f.Id == currentuser().Id);
                    var part = user.Subscriptions.First(f => f.EventID == id);
                    part.FriendCount = (amount>0) ? amount : 0;
                    context.Accounts.Update(user);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = e.Message });
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
                        context.Accounts.Update(cuser);
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
                var parti = user.Subscriptions.Where(f => f.Payed == 0).ToList();

                if (parti.Count() < 1)
                    return Json(new { status = "error", error = "Geen openstaande events gevonden" });

                List<int> eventIDS = new List<int>();

                int bill = 0;
                int additions = 0;

                try {

                    foreach (var part in parti)
                    {
                        var eve = context.Events.First(f => f.Id == part.EventID);
                        if (Core.General.DateIsPassed(eve.When))
                        {
                            part.Payed = 1;
                            bill += (eve.Cost * (part.FriendCount+1));

                            additions += part.ConsumptionCount;
                            bill += part.ConsumptionTax;

                            for (int i = 0; i < part.FriendCount+1; i++)
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
                        PayID = Encryption.Random.rHash(currentuser().Token),
                        AdditionsCount = additions
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

        /// <summary>
        /// Create a new ticket
        /// </summary>
        /// <param name="ticket">The ticket object</param>
        /// <returns>json, status: success or status:error</returns>
        [HttpPost("Tickets/createTicket")]
        public IActionResult CreateTicket(Ticket ticket)
        {
            try
            {
                Classes.Cryptography.Encryption e = new Classes.Cryptography.Encryption(Aes.Create(), Core.General.Credentials.mailPass, "Over$tagSALT");
                using (var context = new OverstagContext())
                {
                    ticket.Timestamp = DateTime.Now;
                    ticket.UserID = currentuser().Id;
                    ticket.Message = e.Encrypt(ticket.Message);
                    context.Tickets.Add(ticket);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = e });
            }
        }

        /// <summary>
        /// Get tickets in View
        /// </summary>
        /// <param name="all">defines if top(4) or all (enter random number)</param>
        /// <returns>View with tickets</returns>
        [Route("Tickets/{all?}")]
        public IActionResult Tickets(int? all)
        {
            var user = currentuser();
            ViewBag.Type = user.Type;
            if (user == null)
                return Content("<h1 style=\"color: red;\">Je moet ingelogd zijn</h1>", "text/html");
            try
            {
                ViewBag.AllowTickets = (user.DenyTickets == 0);
                if (all == null)
                {
                    ViewBag.All = false;
                    return View(getTickets(user.Type));
                }
                else
                {
                    ViewBag.All = true;
                    return View(getTickets(user.Type, true));
                }
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = e });
            }
        }

        /// <summary>
        /// Create a list with tickets
        /// </summary>
        /// <param name="usertype">The type</param>
        /// <param name="all">defines if you want top(4) or all records. Default false</param>
        /// <returns>List(Ticket)</returns>
        private List<Ticket> getTickets(int usertype, bool all = false)
        {
            List<Ticket> tickets = new List<Ticket>();
            Classes.Cryptography.Encryption e = new Classes.Cryptography.Encryption(Aes.Create(), Core.General.Credentials.mailPass, "Over$tagSALT");
            using (var context = new OverstagContext())
            {
                var ticketss = new List<Ticket>();
                if (!all)
                    ticketss = context.Tickets.Where(f => f.Type == usertype).OrderBy(f => f.Timestamp).ToArray().Reverse().ToList().Take(4).ToList();
                else
                    ticketss = context.Tickets.Where(f => f.Type == usertype).OrderBy(f => f.Timestamp).ToArray().Reverse().ToList().ToList();

                foreach (var ticket in ticketss)
                {
                    ticket.Message = e.Decrypt(ticket.Message);
                    tickets.Add(ticket);
                }
            }
            return tickets;
        }

        /// <summary>
        /// Remember the login and get the remember token
        /// </summary>
        /// <returns>The remember token</returns>
        public IActionResult setRemember()
            => Json(Uri.EscapeDataString(Core.Auth.Register(HttpContext.Connection.RemoteIpAddress.ToString(), currentuser().Id)));

        /// <summary>
        /// Remove rememberance with specified token
        /// </summary>
        /// <returns>json, success or error</returns>
        [HttpGet("User/removeRemember/{token}")]
        public IActionResult removeRemember(string token)
        {
            if (Core.Auth.UnRegister(Uri.UnescapeDataString(token)))
                return Json(new { status = "success" });
            else
                return Json(new { status = "error" });
        }

        public IActionResult removeAuth()
        {
            int cnt = Core.Auth.ClearUser(currentuser().Id);
            return Json(new { status = "success", cnt });
        }

        /// <summary>
        /// Try to log in with a specified token
        /// </summary>
        /// <param name="token">The auth token</param>
        /// <returns>json, success or error</returns>
        [HttpGet("Register/tryRestore/{token}")]
        public IActionResult tryRestore(string token)
        {
            token = Uri.UnescapeDataString(token);
            if (Core.Auth.IsAuthenticated(token))
            {
                try
                {
                    int id = Core.Auth.getUserID(token);
                    Core.Auth.UnRegister(token);
                    string newtoken = Core.Auth.Register(HttpContext.Connection.RemoteIpAddress.ToString(), id);
                    using (var context = new OverstagContext())
                    {
                        var account = context.Accounts.First(f => f.Id == id);
                        HttpContext.Session.SetString("Token", account.Token);
                        HttpContext.Session.SetInt32("Type", account.Type);
                        HttpContext.Session.SetString("Name", account.Username);
                    }
                    return Json(new { status = "success", newtoken});
                }
                catch
                {
                    return Json(new { status = "error", error = "Er is iets fout gegaan" });
                }
                
            }
            else
                return Json(new { status = "error", error = "Token is onjuist" });
        }

        public JsonResult getAuth()
            => Json(Core.Auth.getAuth(currentuser().Id));
    }
}