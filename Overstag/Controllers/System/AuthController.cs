using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;

namespace Overstag.Controllers
{
    public class AuthController : OverstagController
    {
        /// <summary>
        /// Register new loginToken
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <returns>JSON {status, token} -> new token to login</returns>
        [HttpPost]
        public async Task<JsonResult> Register([FromForm] string token)
        {
            token = Uri.UnescapeDataString(token);

            string ttoken = await Security.Auth.Register(token, HttpContext.Connection.RemoteIpAddress.ToString());
            return Json(new {status = "success", token = Uri.EscapeDataString(ttoken)});
        }

        /// <summary>
        /// Login into system using Auth token
        /// </summary>
        /// <param name="token">The auth token</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpPost]
        public async Task<JsonResult> Login([FromForm] string token)
        {
            token = Uri.UnescapeDataString(token);
            await using var context = new OverstagContext();
            if (context.Auths.Any(f => f.Token == token))
            {
                var auth = await context.Auths.Include(f => f.User).FirstAsync(f => f.Token == token);
                if (auth.Registered > DateTime.Now.AddMonths(-2))
                {
                    var user = await context.Accounts.FirstAsync(f => f.Id == auth.User.Id);

                    //Important session variables
                    base.setUser(user);

                    return Json(new {status = "success"});
                }
                else
                {
                    try
                    {
                        context.Auths.Remove(auth);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception e)
                    {
                        return Json(new {status = "error", error = e.Message});
                    }
                }
            }

            return Json(new {status = "error"});
        }
    }
}