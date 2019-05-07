using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using Newtonsoft.Json;


namespace Overstag.Controllers
{
    public class UserController : Controller
    {
        //gets the current user
        public Account currentuser() { using (var c = new OverstagContext()) { try { return c.Accounts.Where(a => a.Token.Equals(HttpContext.Session.GetString("Token"))).FirstOrDefault(); } catch { return null; } } }

        public IActionResult Index() {return View(currentuser());}

        public IActionResult Settings(){return View(currentuser());}
        public IActionResult Events() { return View(); }

        //Partial view
        public IActionResult Subscriptions()
        {
            using (var context = new OverstagContext())
            {
                var parti = context.Participate.Where(p => p.UserId == currentuser().Id).ToList();
                Dictionary<Event, bool> events = new Dictionary<Event, bool>();
                events.Clear();

                foreach(var part in parti)
                {
                    bool test = false;
                    if(!events.TryGetValue(context.Events.Where(e => e.Id == part.EventId).FirstOrDefault(),out test) && part != null)
                    {
                        events.Add(context.Events.Where(e => e.Id == part.EventId).FirstOrDefault(), (part.Payed == 0 ? false : true));
                    }
                }

                return View(events);
            }
        }


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

        [HttpPost]
        public async Task<IActionResult> postDeleteAccount(Account a)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    
                    var account = context.Accounts.Where(e => e.Token == a.Token).FirstOrDefault();
                    if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                    {
                        try
                        {
                            //Ingeschreven events verwijderen
                            try
                            {
                                var subscriptions = context.Participate.Where(p => p.UserId == account.Id).ToList();
                                foreach(var item in subscriptions)
                                {
                                    context.Remove(item);
                                }
                            }
                            catch
                            {

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
                catch { return Json(new { status = "error", error = "Token bestaat niet" }); }
            }


        }

    }
}