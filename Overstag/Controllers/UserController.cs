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
        //gets the current user
        private Account currentuser() { using (var c = new OverstagContext()) { try { return c.Accounts.Where(a => a.Token.Equals(HttpContext.Session.GetString("Token"))).FirstOrDefault(); } catch { return null; } } }

        public IActionResult Index() {return View(currentuser());}

        public IActionResult Settings(){return View(currentuser());}

        [HttpGet]
        public IActionResult Events()
        {
            using (var context = new OverstagContext())
            {
                var parti = context.Participate.Where(p => p.User.Id == currentuser().Id).ToList();
                Dictionary<Event, bool> events = new Dictionary<Event, bool>();
               /* foreach(var part in parti)
                {
                    if(part != null)
                    {
                        events.Add(context.Events.Where(e => e.Id == part.EventId).FirstOrDefault(),(part.Payed == 0 ? false : true));
                    }
                }*/
                //Bovenstaande werkt

                //Onderstaande gaat alles fout
                var eve = context.Events.Select(e => e.Participants.Where(a => a.User.Id == currentuser().Id).ToList());
                //2x first or default???
                foreach(var ele in eve)
                {
                    return Json(new { eve = ele.FirstOrDefault().EventId}); //Dit werkt wel
                    events.Add(context.Events.Where(e => e.Id.Equals(ele.FirstOrDefault().Event.Id)).FirstOrDefault(), false);
                }
                return View(eve);

            }
        }

        [HttpPost]
        public async Task<IActionResult> postSubscribeEvent(Participate p)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    /*var user = context.Accounts.Where(u => u.Id == currentuser().Id).FirstOrDefault();
                    Event eve = context.Events.Where(e => e.Id == t.Id).FirstOrDefault();

                    Participate p = new Participate();
                    p.User = user;
                    p.UserId = user.Id;
                    p.Event = eve;
                    p.EventId = eve.Id;
                    eve.Participants.Add(p);

                    //Werkt, maar niet relationeel??

                    context.Update(eve);
                    await context.SaveChangesAsync();*/
                    p.UserId = currentuser().Id;

                    var user = context.Accounts.Where(a => a.Id == p.UserId).FirstOrDefault();
                    //add 1 event to one subscription
                    var eve = context.Events.Where(a => a.Id == p.EventId).FirstOrDefault();
                    if (user == null)
                    {
                        return Json(new { status = "error", error = "USER IS NULL"  });
                    }
                    else if (eve == null)
                    {
                        return Json(new { status = "error", error = "EVENT IS NULL"  });
                    }

                    var subscription = new Participate { User = user, Event = eve, UserId = user.Id, EventId = eve.Id, Payed = 0 };

                    //Object reference not set to an instance of an object. ERROR
                    user.Participants.Add(subscription);

                    context.Accounts.Update(user);

                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = e.ToString() });
                }
            }
        }

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
                        context.Update(cuser);
                        await context.SaveChangesAsync();
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
        [HttpPost]
        public async Task<IActionResult> postInfoChange(Account a)
        {
            using (var context = new OverstagContext())
            {
                var cuser = context.Accounts.Where(x => x.Token == currentuser().Token).FirstOrDefault();
                cuser.Firstname = a.Firstname;
                cuser.Lastname = a.Lastname;
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

    }
}