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

namespace Overstag.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
            => View("Login");

        [Route("Register/Login")]
        [Route("inloggen")]
        public IActionResult Login([FromQuery]string r)
            => View("Login", (r==null)?"":r);

        [Route("Register/Register")]
        [Route("aanmelden")]
        public IActionResult Register()
            => View();

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

        public async Task<IActionResult> Logout([FromQuery]string token)
        {
            //Important session variables
            HttpContext.Session.Remove("Token");
            HttpContext.Session.Remove("Type");

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
                    try { testun = context.Accounts.Where(a => a.Username == account.Username).FirstOrDefault().Username; } catch { testun = ""; }
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
                            HttpContext.Session.SetString("Token", account.Token);
                            HttpContext.Session.SetInt32("Type", account.Type);

                            string remember = await Security.Auth.Register(account.Token, HttpContext.Connection.RemoteIpAddress.ToString());

                            return Json(new { status = "success", remember = Uri.EscapeDataString(remember) });
                        }
                        catch (Exception e)
                        {
                            return Json(new { status = "error", error = "Registratie is mislukt door interne fout.\nNeem a.u.b contact met ons op.", debuginfo = e, code = 2 });
                        }

                    }
                }

            }
            else { return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer of alle velden juist zijn ingevuld" }); }
        }

        /// <summary>
        /// Tries to sign in into the system
        /// </summary>
        /// <param name="a">The login info</param>
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
                                //Login is juist, redirect naar page, zet sessie variablen

                                if (no2fa) //door de sessievariablen niet te setten bij 2fa ben je alsnog niet ingelogd
                                {
                                    //Set important session variables
                                    HttpContext.Session.SetString("Token", account.Token);
                                    HttpContext.Session.SetInt32("Type", account.Type);
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
        /// <param name="a">The account info (email adress)</param>
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
        /// <returns>Json (status = success or error)</returns>
        [HttpPost]
        [Route("Register/Validate2FA")]
        public JsonResult Validate2FA([FromForm]string token, [FromForm]string code, [FromForm]string datetime)
        {
            DateTime now = DateTime.ParseExact(datetime, "dd-MM-yyyy HH:mm:ss",CultureInfo.InvariantCulture);

            if(now < DateTime.Now.AddMinutes(-2))
                return Json(new { status = "timeout" });

            token = Uri.UnescapeDataString(token);
            if (Security.TFA.Validate(code, token,now))
            {
                using(var context = new OverstagContext())
                {
                    var a = context.Accounts.First(e => e.Token == token);

                    //Set important session variables
                    HttpContext.Session.SetString("Token", a.Token);
                    HttpContext.Session.SetInt32("Type", a.Type);

                    string remember = Security.Auth.Register(a.Token,HttpContext.Connection.RemoteIpAddress.ToString()).Result;
                    HttpContext.Session.SetString("Remember", remember);
                    return Json(new { status = "success", remember = Uri.EscapeDataString(remember) });
                }
            }                
            else
                return Json(new { status = "error" });
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
        [HttpGet("Register/joinFamily/{token}")]
        public async Task<IActionResult> JoinFamily(string token)
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                HttpContext.Session.SetString("JoinFamily",token);
                return View("Login","/Register/joinFamily/"+token);
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
                        var user = context.Accounts.First(f => f.Token == HttpContext.Session.GetString("Token"));
                        if(user.Id == family.ParentID)
                        {
                            string[] error = { "U kunt uzelf niet koppelen aan uw eigen gezin.", "<i>Log uzelf uit en probeer de link opnieuw te openen.</i>", "<br><button class=\"btn btn-large blue waves-effect center-align\" onclick=\"OverstagJS.General.logout();\">Uitloggen</button>" };
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

    }
}