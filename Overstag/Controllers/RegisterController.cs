using System;
using System.Collections.Generic;
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
                    //context.Database.EnsureCreatedAsync();
                    string testun = "";
                    try { testun = context.Accounts.Where(a => a.Username == account.Username).FirstOrDefault().Username; } catch { testun = ""; }
                    if (!string.IsNullOrEmpty(testun))
                    {
                        ViewBag.Message = "[Err]Gebruiker bestaat al!";
                    }
                    else
                    {
                        account.Password = Overstag.Encryption.PBKDF2.Hash(account.Password); //Create hash of password
                        account.Token = Overstag.Encryption.SHA.S256(Encryption.Random.rString(50));
                        context.Add(account);
                        await context.SaveChangesAsync();
                        ViewBag.Message = "Registratie successvol!";
                    }
                }

            }
            else { ViewBag.Message = "[Err]Model is niet geldig!"; }
            return View("Register");
        }

        [HttpPost]
        public IActionResult postLogin(Account a)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "[Err]Model niet geldig";
            }
            else
            {
                using (var context = new AccountContext())
                {
                    try
                    {
                        var account = context.Accounts.Where(e => e.Username == a.Username || e.Email == a.Email).FirstOrDefault();
                        if (Overstag.Encryption.PBKDF2.Verify(account.Password, a.Password))
                        {
                            //Login is juist, redirect naar page
                            HttpContext.Session.SetString("Token", account.Token);
                            ViewBag.Message = "OK";
                        }
                        else { ViewBag.Message = "[Err]Gebruikersnaam of wachtwoord is onjuist"; }
                    }
                    catch { ViewBag.Message = "[Err]Gebruiker bestaat niet"; }
                }
            }
            return View("Login");

        }
    }
}