using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.NoDB;
using System.Net;
using System.Text;
using System.IO;
using System.Web;
using Overstag.Authorization;

namespace Overstag.Controllers
{
    [OverstagAuthorize]
    public class UserController : OverstagController
    {
       /// <summary>
        /// Get user homepage
        /// </summary>
        /// <returns>User homepage (view)</returns>
        public IActionResult Index() => View(currentUser);


       /// <summary>
       /// Get settings with user info
       /// </summary>
       /// <returns>View with User modal including family info</returns>
       public async Task<IActionResult> Settings()
       {
          await using var context = new OverstagContext();
          return View(await context.Accounts.Include(f => f.Family).FirstAsync(g => g.Id == currentUser.Id)); 
       }

       /// <summary>
       /// Get saved logins
       /// </summary>
       /// <returns>Logins object</returns>
       public async Task<IActionResult> getLogins()
       {
          await using var context = new OverstagContext();
          return Json(await context.Auths.Where(f => f.UserId == currentUser.Id).OrderByDescending(g => g.Registered).ToListAsync());
       }

        /// <summary>
        /// Remove login by its id
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpGet("User/removeLogin/{id}")]
        public async Task<IActionResult> removeLogin(int id)
        {
            try
            {
                await using var context = new OverstagContext();
                var login = await context.Auths.FindAsync(id);
                context.Auths.Remove(login);
                await context.SaveChangesAsync();
                return Json(new { status = "success" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Verwijderen mislukt door interne fout", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Get events page with all subscriptions
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Events()
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.Include(a => a.Subscriptions).FirstAsync(p => p.Id == currentUser.Id);
            var parti = user.Subscriptions.ToList();

            List<ISubscription> events = new List<ISubscription>();

            foreach (var eve in await context.Events.Where(e => e.When.Date >= DateTime.Today).OrderBy(e => e.When).ToListAsync())
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

        /// <summary>
        /// Get all unPaid events and invoices
        /// </summary>
        /// <returns>View with invoices and unPaid events</returns>
        public async Task<IActionResult> Payment()
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.Include(f => f.Subscriptions).Include(f => f.Family).FirstAsync(f => f.Id == currentUser.Id);
            ViewBag.Age = Core.General.getAge(user.Birthdate);

            List<Event> events = context.Events.ToList();
            List<Event> unPaidEvents = new List<Event>();
            List<Invoice> invoices = await context.Invoices.Include(g => g.User).ThenInclude(h => h.Subscriptions).Where(f => f.User.Id == user.Id).ToListAsync();
            List<XInvoice> iinvoices = new List<XInvoice>();

            foreach(var p in user.Subscriptions)
            {
                if(!p.Paid)
                {
                    var e = events.First(f => f.Id == p.EventID);
                    if (!unPaidEvents.Contains(e) && Core.General.DateIsPassed(e.When))
                        unPaidEvents.Add(e);
                }
            }

            if(invoices.Any())
            {
                foreach(var i in invoices)
                    iinvoices.Add(Services.Invoices.GetXInvoice(i));
            }

            ViewBag.HasFamily = (user.Family != null);
            ViewBag.IsParent = (user.Type == 1);

            return View("Payment", new UnPaidEvents()
            {
                Invoices = iinvoices,
                Subscriptions = user.Subscriptions,
                UnfacturedEvents = unPaidEvents.OrderBy(f => f.When).ToList()
            });
        }


        /// <summary>
        /// Get all subscribers of an event
        /// </summary>
        /// <param name="eventID">The event's id</param>
        /// <returns>JSON serialized</returns>
        [Route("User/getSubscribers/{eventID}")]
        public async Task<JsonResult> getSubscribers(int eventID)
        {
            await using var context = new OverstagContext();
            try
            {
                List<int> subs = (await context.Events.Include(f => f.Participators)
                    .FirstAsync(g => g.Id == eventID)).Participators.Select(f => f.UserID).ToList();
                subs.Remove(currentUser.Id);
                List<string> Users = context.Accounts.Where(f => subs.Contains(f.Id)).Select(f => $"{f.Firstname} {f.Lastname}").ToList();
                return Json(new { status = "success", data = Users.ToArray() });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", debuginfo = e.Message });
            }
        }

        /// <summary>
        /// Renders all ideas with up and downvotes from user
        /// </summary>
        /// <returns>A View with votes</returns>
        public async Task<IActionResult> Ideas()
        {
            await using var context = new OverstagContext();
            ViewBag.UserID = currentUser.Id;
            return View(await context.Ideas.Include(f => f.Votes).OrderByDescending(b => (b.Votes.Count(i => i.Upvote)- b.Votes.Count(i => !i.Upvote))).ToListAsync());
        }

        public IActionResult Vote() 
            => View();

        public async Task<IActionResult> Declaration()
        {
            await using var context = new OverstagContext(); 
            return View(await context.Transactions.Include(x => x.User).Where(f => f.User.Id == currentUser.Id && f.Type == 29).OrderByDescending(g => g.Timestamp).ToListAsync());
        }

        /// <summary>
        /// Post a new idea to the server
        /// </summary>
        /// <param name="idea">The formencoded Idea</param>
        /// <returns>JSON (status = "success" or "error")</returns>
        [HttpPost("User/Vote/postIdea")]
        public async Task<IActionResult> postIdea(Idea idea)
        {
            if (currentUser.Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet een idee indienen." });

            try
            {
                await using (var context = new OverstagContext())
                {
                    await context.Ideas.AddAsync(idea);
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
            if (currentUser.Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u niet stemmen voor een activiteit." });

            await using var context = new OverstagContext();
            try
            {
                var user = await context.Accounts.Include(f => f.Votes).FirstAsync(u => u.Id == currentUser.Id);

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

        /// <summary>
        /// Adds an subscription to the user for an upcoming event
        /// </summary>
        /// <param name="p">The EventID</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postSubscribeEvent(Participate p)
        {
            if (currentUser.Type == 1)
                return Json(new { status = "error", error = "Als ouder kunt u zich niet inschrijven voor een activiteit." });

            await using var context = new OverstagContext();
            try
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstAsync(a => a.Id == currentUser.Id);
                var eve = context.Events.First(e => e.Id == p.EventID);

                if (DateTime.Today > eve.When.Date)
                    return Json(new { status = "error", error = "Deze activiteit is al voorbij. U kunt zich hiervoor niet meer inschrijven" });

                var part = user.Subscriptions.FirstOrDefault(e => e.EventID == p.EventID);
                if(part == null)
                {
                    user.Subscriptions.Add(new Participate { UserID = user.Id, EventID = eve.Id });
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                else return Json(new { status = "error", error = "Je bent al ingeschreven voor deze activiteit!" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
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
            await using var context = new OverstagContext();
            try
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstAsync(a => a.Id == currentUser.Id);
                var eve = await context.Events.FirstAsync(e => e.Id == p.EventID);

                if (Core.General.DateIsPassed(eve.When))
                    return Json(new { status = "error", error = "Dit event is al voorbij. U kunt zich hiervoor niet meer uitschrijven" });

                try
                {
                    var part = user.Subscriptions.FirstOrDefault(e => e.EventID == p.EventID);
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

        /// <summary>
        /// Increase or decrease FriendCount in subscription model
        /// </summary>
        /// <param name="id">The event's id</param>
        /// <param name="incr">The incrementer</param>
        /// <returns>JSON result, status=error or status=success</returns>
        [HttpPost]
        public async Task<IActionResult> SubscribeFriends([FromForm]int id, [FromForm]int amount)
        {
            await using var context = new OverstagContext();
            try
            {
                var user = await context.Accounts.Include(f => f.Subscriptions).FirstAsync(f => f.Id == currentUser.Id);
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

        [HttpPost]
        public async Task<IActionResult> postDeclaration(Accountancy.Transaction request)
        {
            try
            {
                request.Timestamp = DateTime.Now;
                request.Paid = false;
                request.UserId = currentUser.Id;
                request.Type = 29; //Declaratie
                request.Amount = request.Amount * -1;
                await using var context = new OverstagContext();
                await context.Transactions.AddAsync(request);
                await context.SaveChangesAsync();
                return Json(new { status = "success" });
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
            await using var context = new OverstagContext();
            var cuser = currentUser;
            if (Encryption.PBKDF2.Verify(cuser.Password, Oldpass))
            {
                try
                {
                    cuser.Password = Encryption.PBKDF2.Hash(Newpass);
                    cuser.Token = Encryption.Random.rHash(Encryption.SHA.S256(cuser.Firstname) + cuser.Username);
                    context.Accounts.Update(cuser);
                    await context.SaveChangesAsync();
                    base.setUser(cuser);

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

        /// <summary>
        /// Changes the account info of an user
        /// </summary>
        /// <param name="a">Updated account info</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postInfoChange(Account a)
        {
            await using var context = new OverstagContext();
            var cuser = await context.Accounts.FirstAsync(f => f.Id == currentUser.Id);
            cuser.Firstname = a.Firstname;
            cuser.Lastname = a.Lastname;
            cuser.Adress = a.Adress;
            cuser.Residence = a.Residence;
            cuser.Postalcode = a.Postalcode;
            cuser.Phone = a.Phone;

            //Check if email is in use
            var x = await context.Accounts.FirstOrDefaultAsync(f => f.Email == a.Email);
            if (x != null && x.Token != cuser.Token)
                return Json(new { status = "error", error = "Emailadres is al in gebruik!" });
            else
                cuser.Email = a.Email;

            //Check if username is in use
            var y = await context.Accounts.FirstOrDefaultAsync(f => f.Username == a.Username);
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

        /// <summary>
        /// Generates invoice from all unPaid events of current user
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        [HttpGet]
        public async Task<JsonResult> GenerateInvoice()
        {
            bool result = await Services.Invoices.Create(currentUser.Id);
            if(result)
                return Json(new { status = "success" });
            else
                return Json(new { status = "error", error = Services.Invoices.error.ToString() });
        }

        public async Task<JsonResult> MergeInvoices()
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.Include(f => f.Invoices).FirstAsync(f => f.Id == currentUser.Id);
            var invoices = user.Invoices.Where(f => !f.Paid).ToList();
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
                i.Paid = false;
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

        /// <summary>
        /// Toggles the 2FA for the current user
        /// </summary>
        /// <returns>Json (status = success with the secret or status = error)</returns>
        [HttpGet]
        public JsonResult Toggle2FA()
        {
            if (string.IsNullOrEmpty(currentUser.TwoFactor))
            {
                //aanzetten
                setUser(Security.TFA.GenerateSecretForAccount(currentUser.Token));
                return Json(new { status = "success", secret = currentUser.TwoFactor });
            }
            else
            {
                if (Security.TFA.Disable(currentUser.Token))
                {
                    var user = currentUser;
                    user.TwoFactor = null;
                    user.TwoFactorCodes = null;
                    setUser(user);
                    return Json(new { status = "success", secret = "" });
                }
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
            if (!string.IsNullOrEmpty(currentUser.TwoFactor))
            {
                string secret = Security.TFA.GetSecret(currentUser.Token);
                return Json(new { status = "success", secret });
            }
            else
            {
                return Json(new { status = "error", error = "2FA is niet ingeschakeld" });
            }
        }

        

        /// <summary>
        /// Get auth code
        /// </summary>
        /// <param name="randomToken">The verification random token to verify if request is from user</param>
        /// <returns>JSON with token, or header, or posts to callback url</returns>
        [HttpPost]
        public async Task<JsonResult> GetAPIKey([FromForm]string randomToken)
        {
            string callbackUrl = HttpContext.Session.GetString("CallbackUrl");
            string authToken = HttpContext.Session.GetString("AuthToken");

            HttpContext.Session.Remove("AuthToken");
            HttpContext.Session.Remove("CallbackUrl");

            if (!string.IsNullOrEmpty(randomToken) && authToken == randomToken)
            {
                string token = await Security.Auth.CreateAPIToken(currentUser.Token);
                
                if (!string.IsNullOrEmpty(callbackUrl))
                {
                    if(callbackUrl != "ANDROID")
                    {
                        try
                        {
                            var uri = new UriBuilder(callbackUrl);
                            var query = HttpUtility.ParseQueryString(uri.Query);
                            query["apiKey"] = token;

                            var data = Encoding.UTF8.GetBytes(query.ToString().Replace("?",""));
                            uri.Query = query.ToString();

                            var request = (HttpWebRequest)WebRequest.Create(uri.ToString());

                            request.Method = "POST";
                            request.ContentType = "application/x-www-form-urlencoded";
                            request.ContentLength = data.Length;

                            await using (var stream = request.GetRequestStream())
                                await stream.WriteAsync(data, 0, data.Length);

                            var response = (HttpWebResponse)request.GetResponse();
                            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                            return Json(new { status = "success", calledUrl= uri.ToString(), response = responseString });
                        }
                        catch (Exception e)
                        {
                            return Json(new { status = "warning", error = e.ToString() });
                        }
                    }
                }

                HttpContext.Response.Headers.Add("Api-Key", token);
                return Json(new { status = "success", apiKey = token });
            }
            else
            {
                return Json(new { status = "error", error = "Human verification failed. Random token (randomToken) was invalid" });
            }
        }

        /// <summary>
        /// Gets 2fa codes (beta)
        /// </summary>
        /// <returns>List with 2fa restore codes</returns>
        public JsonResult Get2FACodes()
            => Json(new { status = "success", data = Security.TFA.GetBackupCodes(currentUser.Token)});

        /// <summary>
        /// Leave the family
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        public async Task<IActionResult> LeaveFamily()
        {
            try
            {
                await using var context = new OverstagContext();
                var user = await context.Accounts.Include(f => f.Family).FirstAsync(u => u.Id == currentUser.Id);
                user.Family = null;
                context.Accounts.Update(user);
                context.SaveChanges();
                return Json(new { status = "success" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", debuginfo = e });
            }
        }
    }
}