using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;

namespace Overstag.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Register([FromForm]string token)
        {
            token = Uri.UnescapeDataString(token);

            string ttoken = await Security.Auth.Register(token, HttpContext.Connection.RemoteIpAddress.ToString());
            return Json(new { status = "success", token = Uri.EscapeDataString(ttoken) });
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromForm]string token)
        {
            return await Task.Run(() => {
                token = Uri.UnescapeDataString(token);
                using (var context = new OverstagContext())
                {
                    if (context.Auths.Any(f => f.Token == token))
                    {
                        var auth = context.Auths.Include(f => f.User).First(f => f.Token == token);
                        if (auth.Registered > DateTime.Now.AddMonths(-2))
                        {
                            var user = context.Accounts.First(f => f.Id == auth.User.Id);
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
                            catch (Exception e)
                            {
                                return Json(new { status = "error", error = e.Message });
                            }
                        }
                    }

                    return Json(new { status = "error" });
                }
            });
        }

        [HttpPost]
        public async Task<JsonResult> Logout()
        {
            if (HttpContext.Session.GetString("Remember") != null)
            {
                string token = HttpContext.Session.GetString("Remember");
                using (var context = new OverstagContext())
                {
                    if (context.Auths.Any(f => f.Token == token))
                    {
                        var auth = await context.Auths.FirstAsync(f => f.Token == token);
                        context.Auths.Remove(auth);
                        await context.SaveChangesAsync();
                        return Json(new { status = "success" });
                    }
                }
            }

            return Json(new { status = "error" });
        }
    }
}
