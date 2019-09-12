using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using Overstag.API;
using Microsoft.AspNetCore.Mvc;

namespace Overstag.Controllers
{
    public class APIController : Controller
    {
        [HttpPost("API/Login")]
        public JsonResult Login(LoginModel data)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var account = context.Accounts.FirstOrDefault(f => f.Username == data.username || f.Email == data.username);

                    if (account == null)
                        return Json(new HttpObject { status = Status.error, message = "Gebruiker bestaat niet" });

                    if (Encryption.PBKDF2.Verify(account.Password, data.password))
                        return Json(new HttpObject { status = Status.success, data = new User { id = account.Id, email = account.Email, firstname = account.Firstname, lastname = account.Lastname, token = account.Token } });
                    else
                        return Json(new HttpObject { status = Status.error, message = "Wachtwoord is onjuist" });
                }
                catch(Exception e)
                {
                    return Json(new HttpObject { status = Status.error, message = e.Message, data = e });
                }
                
            }
        }


    }
}
