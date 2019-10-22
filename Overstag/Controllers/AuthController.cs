using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;

namespace Overstag.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public JsonResult Register([FromForm]string token)
        {
            token = Uri.UnescapeDataString(token);

            string ttoken = Overstag.Security.Auth.Register(token);
            return Json(new { status = "success", token = Uri.EscapeDataString(ttoken) });
        }

        [HttpPost]
        public JsonResult Login([FromForm]string token)
        {
            token = Uri.UnescapeDataString(token);
            using(var context = new OverstagContext())
            {
                if(context.Auths.Any(f => f.Token == token))
                {
                    var auth = context.Auths.First(f => f.Token == token);
                    if(auth.Registered > DateTime.Now.AddMonths(-2))
                    {
                        var user = context.Accounts.First(f => f.Id == auth.UserID);
                        HttpContext.Session.SetString("Token", user.Token);
                        HttpContext.Session.SetInt32("Type", user.Type);
                        HttpContext.Session.SetString("Name", user.Username);
                        HttpContext.Session.SetString("Remember", token);
                        return Json(new { status = "success" });
                    }
                    else
                    {
                        try
                        {
                            context.Auths.Remove(auth);
                            context.SaveChanges();
                        }
                        catch(Exception e) {
                            return Json(new { status = "error", error = e.Message });
                        }
                    }
                }

                return Json(new { status = "error" });
            }
        }

        [HttpPost]
        public JsonResult Logout()
        {
            if (HttpContext.Session.GetString("Remember") != null)
            {
                string token = HttpContext.Session.GetString("Remember");
                using (var context = new OverstagContext())
                {
                    if (context.Auths.Any(f => f.Token == token))
                    {
                        var auth = context.Auths.First(f => f.Token == token);
                        context.Auths.Remove(auth);
                        context.SaveChanges();
                        return Json(new { status = "success" });
                    }
                }
            }

            return Json(new { status = "error" });
        }
    }
}
