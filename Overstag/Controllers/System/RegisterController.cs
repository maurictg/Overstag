#define MOLLIE_ENABLED

//COMMENT this to enable mollie
#undef MOLLIE_ENABLED
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using System.Globalization;

using Mollie.Api;
using Mollie.Api.Models.Customer;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Client;
using System.Text;
using System.Text.Json;
using Overstag.Authorization;

namespace Overstag.Controllers
{
    public class RegisterController : OverstagController
    {
        public IActionResult Index()
            => View("Login");

        [Route("Register/Login")]
        [Route("inloggen")]
        public IActionResult Login([FromQuery]string r)
        {
            ViewBag.RedirectURL = (r == null) ? "" : r;
            return View("Login");
        }

        [Route("Register/Register")]
        [Route("aanmelden")]
        public IActionResult Register()
            => View();

        public IActionResult Privacy()
            => View();

        /// <summary>
        /// Get authenticate page for application
        /// 
        /// Go via: /Register/Authenticate?{params...}
        /// </summary>
        /// <param name="appName">The name of the application that requests the api token</param>
        /// <param name="callbackUrl">The url to send the api token to. Optional, must be URL ENCODED!!!</param>
        /// <returns></returns>
        public IActionResult Authenticate([FromQuery]string appName, [FromQuery]string callbackUrl)
        {
            if(!isLoggedIn)
            {
                ViewBag.RedirectURL = "RELOAD";
                return View("Login");
            }

            string randomToken = Encryption.Random.rString(15);
            callbackUrl = string.IsNullOrEmpty(callbackUrl) ? "" : Uri.UnescapeDataString(callbackUrl);
            appName = (string.IsNullOrEmpty(appName)) ? "Een applicatie" : appName;

            HttpContext.Session.SetString("AuthToken", randomToken);
            HttpContext.Session.SetString("CallbackUrl", callbackUrl);

            ViewBag.Android = !string.IsNullOrEmpty(callbackUrl) && callbackUrl == "ANDROID";
            ViewBag.AuthToken = randomToken;
            ViewBag.AppName = appName;
            ViewBag.CallbackUrl = string.IsNullOrEmpty(callbackUrl) ? "EMPTY" : callbackUrl;

            return View("Authenticate");
        }

        /// <summary>
        /// Takes the user to a page to ask for the email address to send a password reset link
        /// </summary>
        /// <returns>View</returns>
        [Route("Register/Passreset")]
        public IActionResult ResetPassword() => View("ResetPassword");

        /// <summary>
        /// Resets the password of the user
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <returns>View with the password reset fields, or a string with an error</returns>
        [Route("Register/Passreset/{token}")]
        public IActionResult Passreset(string token)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    return View(context.Accounts.First(w => w.Token == Uri.UnescapeDataString(token)));
                }
                catch
                {
                    string[] error = { "Account niet gevonden. Is de link onjuist?", "Controleer de link die we je hebben gestuurd via de mail en plak deze in de adresbalk." };
                    return View("~/Views/Error/Custom.cshtml", error);
                }
            }
        }

        /// <summary>
        /// Logout from the system and remove auth if exists
        /// </summary>
        /// <param name="token">The auth token</param>
        /// <returns></returns>
        public async Task<IActionResult> Logout([FromQuery]string token)
        {
            //Important session variables
            HttpContext.Session.Remove("CurrentUser");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    using (var context = new OverstagContext())
                    {
                        token = Uri.UnescapeDataString(token);
                        if (await context.Auths.AnyAsync(f => f.Token == token))
                        {
                            var auth = await context.Auths.FirstAsync(f => f.Token == token);
                            context.Auths.Remove(auth);
                            await context.SaveChangesAsync();
                            return Json(new { status = "success" });
                        }
                        else
                        {
                            return Json(new { status = "succes", msg = "Token not found." });
                        }
                    }
                }
                catch
                {
                    return Json(new { status = "error" });
                }
            }
            else
            {
                return Json(new { status = "succes", msg = "Token null." });
            }
        }

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
                    try { testun = context.Accounts.Where(a => a.Username.ToLower() == account.Username.ToLower()).FirstOrDefault().Username; } catch { testun = ""; }
                    try { testem = context.Accounts.Where(a => a.Email == account.Email).FirstOrDefault().Email; } catch { testem = ""; }
                    
                    if (!string.IsNullOrEmpty(testun))
                        return Json(new { status = "error", error = "Gebruikersnaam bestaat al!", code = 0 });
                    else if (!string.IsNullOrEmpty(testem))
                        return Json(new { status = "error", error = "Emailadres is al in gebruik!", code = 1 });
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
                            account.Type = (byte)(account.Username.Equals("admin") ? 3 : (account.Type < 2) ? account.Type : 0);
                            account.RegisterDate = DateTime.Now;

                            try
                            {
                                if (account.Type < 2)
                                {
#if MOLLIE_ENABLED
                                    CustomerRequest cr = new CustomerRequest()
                                    {
                                        Name = $"{account.Firstname} {account.Lastname}",
                                        Email = account.Email,
                                        Locale = "nl-NL",
                                        Metadata = account.Token
                                    };

                                    CustomerClient client = new CustomerClient(Core.General.Credentials.mollieApiToken);
                                    CustomerResponse cs = await client.CreateCustomerAsync(cr);

                                    account.MollieID = cs.Id;
#endif
                                }
                            }
                            catch (Exception e)
                            {
                                return Json(new { status = "error", error = "Mollie integratie voor het verwerken van betalingen mislukt, neem contact op met ons.", debuginfo = e.ToString() });
                            }

                            context.Accounts.Add(account);
                            await context.SaveChangesAsync();

                            //Set important session variables
                            base.setUser(account);

                            string remember = await Security.Auth.Register(account.Token, HttpContext.Connection.RemoteIpAddress.ToString());

                            return Json(new { status = "success", remember = Uri.EscapeDataString(remember) });
                        }
                        catch (Exception e)
                        {
                            return Json(new { status = "error", error = "Registratie is mislukt door interne fout.\nNeem a.u.b contact met ons op.", debuginfo = e.ToString(), code = 2 });
                        }

                    }
                }

            }
            else { return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer of alle velden juist zijn ingevuld" }); }
        }

        /// <summary>
        /// Tries to sign in into the system
        /// </summary>
        /// <param name="Username">The username or email address</param>
        /// <param name="Password">The password</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public async Task<JsonResult> postLogin([FromForm]string Username, [FromForm]string Password)
        {
            return await Task.Run(async () =>
            {
                using (var context = new OverstagContext())
                {
                    string ip = HttpContext.Connection.RemoteIpAddress.ToString();
                    try
                    {
                        var account = context.Accounts.FirstOrDefault(e => e.Username.ToLower().Equals(Username.ToLower()) || e.Email.ToLower().Equals(Username.ToLower()));
                        if (account == null)
                            return Json(new { status = "error", error = "Gebruikersnaam of wachtwoord onjuist", debuginfo = "Account is NULL" });

                            if (Encryption.PBKDF2.Verify(account.Password, Password))
                            {
                                bool no2fa = (string.IsNullOrEmpty(account.TwoFactor));
                                if (no2fa)
                                {
                                    base.setUser(account);
                                }

                                string remember = (no2fa) ? await Security.Auth.Register(account.Token, HttpContext.Connection.RemoteIpAddress.ToString()) : "";

                                return Json(new { status = "success", twofactor = (no2fa) ? "no" : "yes", remember = Uri.EscapeDataString(remember), token = (no2fa) ? "" : Uri.EscapeDataString(account.Token), type = account.Type });
                            }
                            else
                            {
                                return Json(new { status = "error", error = "Gebruikersnaam of wachtwoord onjuist" });
                            }
                    }
                    catch (Exception e) { return Json(new { status = "error", error = "Interne fout", innerexception = e.ToString() }); }
                }

            });
        }

        /// <summary>
        /// Sends an email to the user with the password reset info
        /// </summary>
        /// <param name="Email">The user's email adress</param>
        /// <returns>JSON result, status = "error" or status = "success"</returns>
        [HttpPost]
        public JsonResult postMailreset([FromForm]string Email)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var account = context.Accounts.Where(e => e.Email == Email).FirstOrDefault();
                    if (account != null)
                    {
                        var mail = new Services.PassresetMail(account);

                        if (mail.SendAsync().Result)
                            return Json(new { status = "success" });
                        else
                            return Json(new { status = "error", error = "Er is een interne fout opgetreden.", debuginfo = mail.error.ToString() });
                    }
                    else
                    {
                        return Json(new { status = "error", error = "Dit mailadres is niet bekend in ons systeem" });
                    }

                }
                catch (Exception e) { return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() }); }
            }
        }

        /// <summary>
        /// Change password with token.
        /// </summary>
        /// <param name="Token">User's token</param>
        /// <param name="Password">User's new password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> postPassreset([FromForm]string Token, [FromForm]string Password)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    Token = Uri.UnescapeDataString(Token);
                    var account = await context.Accounts.FirstOrDefaultAsync(e => e.Token == Token);

                    if (account == null)
                        return Json(new { status = "error", error = "Token bestaat niet in ons systeem" });

                    account.Password = Encryption.PBKDF2.Hash(Password);
                    account.Token = Encryption.Random.rHash(Encryption.SHA.S256(account.Firstname) + account.Username);
                    context.Accounts.Update(account);
                    await context.SaveChangesAsync();

                    return Json(new { status = "success" });
                }
                catch (Exception e) { return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() }); }
            }
        }

        /// <summary>
        /// Validates the 2fa code
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <param name="code">The validation code</param>
        /// <param name="datetime">The timestamp of the client to avoid time issues</param>
        /// <returns>Json (status = success or error)</returns>
        [HttpPost]
        [Route("Register/Validate2FA")]
        public JsonResult Validate2FA([FromForm]string token, [FromForm]string code, [FromForm]long datetime)
        {
            //DateTime now = DateTime.ParseExact(datetime, "dd-MM-yyyy HH:mm:ss",CultureInfo.InvariantCulture);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(Convert.ToDouble(datetime)).ToLocalTime();

            if (dt < DateTime.Now.AddMinutes(-2))
                return Json(new { status = "timeout" });

            token = Uri.UnescapeDataString(token);
            if (Security.TFA.Validate(code, token, dt))
            {
                using(var context = new OverstagContext())
                {
                    var a = context.Accounts.First(e => e.Token == token);

                    //Set important session variables
                    base.setUser(a);

                    string remember = Security.Auth.Register(a.Token,HttpContext.Connection.RemoteIpAddress.ToString()).Result;
                    HttpContext.Session.SetString("Remember", remember);
                    return Json(new { status = "success", remember = Uri.EscapeDataString(remember) });
                }
            }                
            else
                return Json(new { status = "wrong" });
        }

        /// <summary>
        /// Restores a 2fa backup code
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <param name="code">The restoration code</param>
        /// <returns>Json (status = success with the secret or status = error)</returns>
        [HttpGet]
        [Route("Register/Restore2FA/{token}/{code}")]
        public JsonResult Restore2FA(string token, string code)
        {
            if (Security.TFA.RestoreBackupCode(Uri.UnescapeDataString(code), Uri.UnescapeDataString(token)))
                return Json(new { status = "success"});
            else
                return Json(new { status = "error" });
        }

        /// <summary>
        /// Join a family by its token
        /// </summary>
        /// <param name="token">The family token</param>
        /// <returns>HTML content</returns>
        [OverstagAuthorize]
        [HttpGet("Register/joinFamily/{token}")]
        public async Task<IActionResult> JoinFamily(string token)
        {
            if(!isLoggedIn)
            {
                HttpContext.Session.SetString("JoinFamily",token);
                ViewBag.RedirectURL = "/Register/joinFamily/" + token;
                return View("Login");
            }
            else
            {
                if(!string.IsNullOrEmpty(HttpContext.Session.GetString("JoinFamily")))
                {
                    token = HttpContext.Session.GetString("JoinFamily");
                    HttpContext.Session.Remove("JoinFamily");
                }

                using (var context = new OverstagContext())
                {
                    var family = context.Families.Include(g => g.Members).FirstOrDefault(f => f.Token == Uri.UnescapeDataString(token));

                    if (family == null)
                    {
                        string[] error = { "Familie niet gevonden.", "Waarschijnlijk is de link die je gekregen hebt onjuist. <br/><i>Probeer deze opnieuw te openen</i>" };
                        return View("~/Views/Error/Custom.cshtml", error);
                    }
                    else
                    {
                        var user = currentUser;
                        if(user.Id == family.ParentID)
                        {
                            string[] error = { "U kunt uzelf niet koppelen aan uw eigen gezin.", "<i>Log uzelf uit en probeer de link opnieuw te openen.</i>", "<br><button class=\"btn btn-large blue waves-effect center-align\" onclick=\"Overstag.General.logout();\">Uitloggen</button>" };
                            return View("~/Views/Error/Custom.cshtml", error);
                        }
                        else
                        {
                            user.Family = family;
                            context.Accounts.Update(user);
                            await context.SaveChangesAsync();
                            string[] success = { "Successvol aangemeld bij de familie", "", "<a class=\"btn btn-large blue waves-effect center-align\" href=\"/User\">Klik hier om verder te gaan</a>" };
                            return View("~/Views/Error/Success.cshtml", success);
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Render delete account page
        /// </summary>
        /// <returns></returns>
        [OverstagAuthorize]
        public IActionResult Unregister()
        {
            if(!isLoggedIn)
            {
                ViewBag.RedirectURL = "/Register/Unregister";
                return View("Login");
            }

            var account = new OverstagContext().Accounts.Include(g => g.Invoices).Include(i => i.Subscriptions).First(k => k.Id == currentUser.Id);
            ViewBag.AllowtoDelete = true;
            ViewBag.Name = account.Firstname;

            if (account.Invoices.Count(f => !f.Paid) > 0)
            {
                ViewBag.AllowtoDelete = false;
                ViewBag.Message = "Er staan nog onbetaalde facturen open. Zorg er eerst voor dat deze afgerond zijn en kom dan hier terug.";
            }

            if (account.Subscriptions.Count(f => !f.Paid) > 0)
            {
                ViewBag.AllowtoDelete = false;
                ViewBag.Message = "Er zijn nog ongefactureerde activiteiten of activiteiten waarvoor je nog ingeschreven staat. Zorg er eerst voor dat deze verwijderd/betaald zijn.";
            }

            return View();
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <param name="password">The user's password</param>
        /// <returns>JSON</returns>
        [HttpPost]
        [OverstagAuthorize]
        public async Task<IActionResult> postDeleteAccount([FromForm]string password)
        {
            if (!isLoggedIn)
            {
                return Json(new { status = "error", error = "Je bent niet ingelogd. Log opnieuw in a.u.b." });
            }

            await using var context = new OverstagContext();
            var account = await context.Accounts
                .Include(f => f.Auths)
                .Include(g => g.Invoices)
                .Include(h => h.Payments)
                .Include(i => i.Subscriptions)
                .Include(j => j.Votes)
                .Include(x => x.Transactions)
                .FirstAsync(k => k.Id == currentUser.Id);

            if (!Encryption.PBKDF2.Verify(account.Password, password))
                return Json(new { status = "error", error = "Wachtwoord is onjuist" });

            if (account.Invoices.Count(f => !f.Paid) > 0)
                return Json(new { status = "error", error = "Er staan nog onbetaalde facturen open." });

            if (account.Subscriptions.Count(f => !f.Paid) > 0)
                return Json(new { status = "error", error = "Er staan nog activiteiten open." });

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
                catch(Exception e)
                {
                    return Json(new { status = "warning", warning = "Mollie integratie verwijderen mislukt", debuginfo = e.ToString() });
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

    }
}