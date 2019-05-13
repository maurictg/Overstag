using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index() { return View("Login"); }
        public IActionResult Login() { return View("Login"); }
        public IActionResult Register() { return View(); }

        /// <summary>
        /// Resets the password of the user
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <returns>View with the password reset fields, or a string with an error</returns>
        [HttpGet("Register/Passreset/{token}")]
        public IActionResult Passreset(string token)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    return View(context.Accounts.First(w => w.Token == token));
                }
                catch
                {
                    var content = "<h1>Token is onjuist.</h1><br><p>Controleer de link die we u hebben gestuurt via de email en plak deze link in de adresbalk</p>";

                    return new ContentResult()
                    {
                        Content = content,
                        ContentType = "text/html",
                    };
                }
            }
        }

        public void Logoff() { HttpContext.Session.Remove("Token"); HttpContext.Session.Remove("Name"); Response.Redirect("/Home/Index"); }

        /// <summary>
        /// Register a new user to the system
        /// </summary>
        /// <param name="account">The account information about the new user</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<IActionResult> postRegister(Account account)
        {
            if (ModelState.IsValid)
            {
                using (var context = new OverstagContext())
                {
                    //Check if user already exists
                    string testun = string.Empty;
                    string testem = "";
                    try { testun = context.Accounts.Where(a => a.Username == account.Username).FirstOrDefault().Username; } catch { testun = ""; }
                    try { testem = context.Accounts.Where(a => a.Email == account.Email).FirstOrDefault().Email; } catch { testem = ""; }
                    if (!string.IsNullOrEmpty(testun))
                    {
                        return Json(new { status = "error", error = "Gebruikersnaam bestaat al!", code = 0 });
                    }
                    else if (!string.IsNullOrEmpty(testem))
                    {
                        return Json(new { status = "error", error = "Emailadres is al in gebruik!", code = 1 });
                    }
                    else
                    {
                        try
                        {
                            account.Password = Encryption.PBKDF2.Hash(account.Password); //Create hash of password
                            account.Token = Encryption.SHA.S256(Encryption.Random.rString(50)+account.Username);
                            context.Add(account);
                            await context.SaveChangesAsync();
                            return Json(new { status = "success" });
                        }
                        catch(Exception e)
                        {
                            return Json(new { status = "error", error = "Registratie is mislukt door interne fout.\nProbeer het later opnieuw.", debuginfo = e, code = 2 });
                        }

                    }
                }

            }
            else { return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" }); }
        }

        /// <summary>
        /// Tries to sign in into the system
        /// </summary>
        /// <param name="a">The login info</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public JsonResult postLogin(Login a)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" });
            }
            else
            {
                using (var context = new OverstagContext())
                {
                    try
                    {
                        var account = context.Accounts.Where(e => e.Username == a.Username || e.Email == a.Username).FirstOrDefault();
                        if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                        {
                            //Login is juist, redirect naar page, zet sessie variablen
                            HttpContext.Session.SetString("Token", account.Token);
                            HttpContext.Session.SetString("Name",account.Username);
                            return Json(new { status = "success" });
                        }
                        else { return Json(new { status = "error", error = "Gebruikersnaam of wachtwoord onjuist" }); }
                    }
                    catch { return Json(new { status = "error", error = "Gebruiker bestaat niet" }); }
                }
            }
        }

        /// <summary>
        /// Sends an email to the user with the password reset info
        /// </summary>
        /// <param name="a">The account info (email adress)</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public JsonResult postMailreset(Account a)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" });
            }
            else
            {
                using (var context = new OverstagContext())
                {
                    try
                    {
                        UserController u = new UserController();
                        var account = context.Accounts.Where(e => e.Email == a.Email).FirstOrDefault();
                        if(account != null)
                        {
                            try
                            {
                                string message = "<h1>Overstag wachtwoord reset</h1>" +
                                    "Beste " + account.Firstname + ",<br>We sturen je deze mail omdat je je wachtwoord vergeten bent.<br>" +
                                    "Klik op <a href='/Register/Passreset/" + account.Token + "'>deze link</a>  om je wachtwoord te resetten of plak hem in je adresbalk." +
                                    "<br>Success! Mocht het niet werken, neem dan contact met ons op";
                                string res = Core.Mail.SendMail("Wachtwoord reset", message, account.Email);
                                if (res == "OK")
                                {
                                    return Json(new { status = "success" });
                                }
                                else
                                {
                                    return Json(new { status = "error", error = res });
                                }
                            }
                            catch (Exception e)
                            {
                                return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                            }
                        }
                        else
                        {
                            return Json(new { status = "error", error = "Dit mailadres is niet bekend in ons systeem" });
                        }

                    }
                    catch(Exception e) { return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() }); }
                }
            }
        }

        /// <summary>
        /// Changes the user password
        /// </summary>
        /// <param name="a">Account info (token and new password)</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public JsonResult postPassreset(Account a)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" });
            }
            else
            {
                using (var context = new OverstagContext())
                {
                    try
                    {
                        var account = context.Accounts.Where(e => e.Token == a.Token).FirstOrDefault();
                        try
                        {
                            account.Password = Encryption.PBKDF2.Hash(a.Password);
                            account.Token = Encryption.SHA.S256(Encryption.Random.rString(50)+account.Username);
                            context.Update(account);
                            context.SaveChangesAsync();
                            return Json(new { status = "success" });
                        }
                        catch (Exception e)
                        {
                            return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                        }
                    }
                    catch { return Json(new { status = "error", error = "Token bestaat niet in ons systeem" }); }
                }
            }
        }
    }
}