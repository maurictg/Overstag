using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;


namespace Overstag.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index() => View("Login");
        public IActionResult Login() => View("Login","");
        public IActionResult Register() => View();

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
                    return View(context.Accounts.First(w => w.Token == Uri.UnescapeDataString(token)));
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

        public void Logoff() { HttpContext.Session.Remove("Token"); HttpContext.Session.Remove("Name"); HttpContext.Session.Remove("Type"); Response.Redirect("/Home/Index"); }

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
                            //Make name uppercase
                            account.Firstname = account.Firstname[0].ToString().ToUpper() + account.Firstname.Substring(1);

                            char[] l = account.Lastname.ToCharArray();
                            int i = (account.Lastname.Contains(' ')) ? account.Lastname.TrimEnd(' ').LastIndexOf(' ') + 1 : 0;
                            l[i] = char.ToUpper(l[i]);
                            account.Lastname = new string(l);

                            //Set password hashes, create token and set type
                            account.Password = Encryption.PBKDF2.Hash(account.Password);
                            account.Token = Encryption.Random.rHash(Encryption.SHA.S256(account.Firstname) + account.Username);
                            account.Type = (account.Username.Equals("admin") ? 3 : (account.Type < 2) ? account.Type : 0);

                            context.Accounts.Add(account);
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
                    string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    try
                    {
                        var account = context.Accounts.Where(e => e.Username == a.Username || e.Email == a.Username).FirstOrDefault();
                        //Controleren op onjuiste inlogpogingen
                        if(context.Logging.Count(i=>i.Ip==ip && i.Date == DateTime.Now.Date && i.Username==a.Username) > 15)
                        {
                            return Json(new { status = "error", error = "Te veel onjuiste inlogpogingen. Probeer het morgen opnieuw." });
                        }
                        else
                        {
                            if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                            {
                                bool no2fa = (string.IsNullOrEmpty(account.TwoFactor));
                                //Login is juist, redirect naar page, zet sessie variablen


                                if (no2fa) //door de sessievariablen niet te setten bij 2fa ben je alsnog niet ingelogd
                                {
                                    HttpContext.Session.SetString("Token", account.Token);
                                    HttpContext.Session.SetInt32("Type", account.Type);
                                    HttpContext.Session.SetString("Name", account.Username);
                                }

                                //Eventuele foute pogingen verwijderen
                                if (context.Logging.Count(i => i.Ip == ip && i.Username == a.Username) > 0)
                                {
                                    foreach (var log in context.Logging.Where(i => i.Ip == ip && i.Username == a.Username))
                                        context.Logging.Remove(log);

                                    context.SaveChanges();
                                }

                                return Json(new { status = "success", twofactor = (no2fa) ? "no" : "yes", token = (no2fa) ? "" : Uri.EscapeDataString(account.Token) });
                            }
                            else
                            {
                                context.Logging.Add(new Logging { Ip = ip, Type = 0, Username = a.Username, Date = DateTime.Now.Date });
                                context.SaveChanges();
                                return Json(new { status = "error", error = "Gebruikersnaam of wachtwoord onjuist" });
                            }
                        }
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
                                    "Klik op <a href='/Register/Passreset/" + Uri.EscapeDataString(account.Token) + "'>deze link</a>  om je wachtwoord te resetten of plak hem in je adresbalk." +
                                    "<br>Success! Mocht het niet werken, neem dan contact met ons op";
                                string res = Core.General.SendMail("Wachtwoord reset", message, account.Email);
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
            a.Token = Uri.UnescapeDataString(a.Token);
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
                            account.Token = Encryption.Random.rHash(Encryption.SHA.S256(account.Firstname) + account.Username);
                            context.Update(account);
                            context.SaveChangesAsync();

                            if(HttpContext.Session.GetString("Token")==a.Token)
                                HttpContext.Session.SetString("Token", account.Token);
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

        [HttpGet]
        [Route("Register/Validate2FA/{token}/{code}")]
        public JsonResult Validate2FA(string token, string code)
        {
            token = Uri.UnescapeDataString(token);
            if (Security.TFA.Validate(code, token))
            {
                using(var context = new OverstagContext())
                {
                    var a = context.Accounts.First(e => e.Token == token);
                    HttpContext.Session.SetString("Token", a.Token);
                    HttpContext.Session.SetInt32("Type", a.Type);
                    HttpContext.Session.SetString("Name", a.Username);
                }
                return Json(new { status = "success" });
            }                
            else
                return Json(new { status = "error" });
        }

        [HttpGet]
        [Route("Register/Restore2FA/{token}/{code}")]
        public JsonResult Restore2FA(string token, string code)
        {
            if (Security.TFA.RestoreBackupCode(Uri.UnescapeDataString(code), Uri.UnescapeDataString(token)))
                return Json(new { status = "success", secret = Uri.EscapeDataString(new OverstagContext().Accounts.First(t => t.Token == Uri.UnescapeDataString(token)).TwoFactor) });
            else
                return Json(new { status = "error" });
        }
         


        /// <summary>
        /// Join a family by its token
        /// </summary>
        /// <param name="token">The family token</param>
        /// <returns>HTML content</returns>
        [HttpGet("Register/joinFamily/{token}")]
        public IActionResult JoinFamily(string token)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                return View("Login","/Register/joinFamily/"+token);
            }
            else
            {
                using (var context = new OverstagContext())
                {
                    var content = "";
                    var family = context.Families.Include(g => g.Members).FirstOrDefault(f => f.Token == Uri.UnescapeDataString(token));

                    if (family == null)
                        content = "<h1 style=\"color: red;\">Token is onjuist of famillie bestaat niet</h1><br><p>Controleer de link die je hebt gekregen.</p>";
                    else
                    {
                        var user = context.Accounts.First(f => f.Token == HttpContext.Session.GetString("Token"));
                        user.Family = family;
                        context.Accounts.Update(user);
                        context.SaveChanges();
                        content = "<h1 style=\"color: green;\">Gelukt!!!</h1><h1>U bent nu lid van de famillie</h1>" +
                            "<br><a href=\"/User\">Klik hier om verder te gaan</a>";
                    }

                    return new ContentResult()
                    {
                        Content = content,
                        ContentType = "text/html",
                    };
                }
            }
        }

    }
}