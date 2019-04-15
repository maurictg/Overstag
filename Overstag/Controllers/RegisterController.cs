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
        public IActionResult Register() { return View("Register"); }
        public void Logoff() { HttpContext.Session.Remove("Token"); Response.Redirect("/Home/Index"); }

        [HttpPost]
        public async Task<IActionResult> postRegister(Account account)
        {
            if (ModelState.IsValid)
            {
                using (var context = new AccountContext())
                {
                    //Check if user already exists
                    string testun = "";
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
                            account.Token = Encryption.SHA.S256(Encryption.Random.rString(50));
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


        [HttpPost]
        public JsonResult postLogin(Account a)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { status = "error", error = "Gegevens zijn ongeldig.\nControleer alle velden" });
            }
            else
            {
                using (var context = new AccountContext())
                {
                    try
                    {
                        var account = context.Accounts.Where(e => e.Username == a.Username || e.Email == a.Username).FirstOrDefault();
                        if (Encryption.PBKDF2.Verify(account.Password, a.Password))
                        {
                            //Login is juist, redirect naar page, zet sessie variablen
                            HttpContext.Session.SetString("Token", account.Token);
                            HttpContext.Session.SetString("Name",account.Firstname);
                            return Json(new { status = "success" });
                        }
                        else { return Json(new { status = "error", error = "Gebruikersnaam of wachtwoord onjuist" }); }
                    }
                    catch { return Json(new { status = "error", error = "Gebruiker bestaat niet" }); }
                }
            }
        }
    }
}