using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;
using Overstag.Models.Database;

namespace Overstag.Controllers
{
    public class AuthController : OverstagController
    {
        /// <summary>
        /// Login into system using Auth token
        /// </summary>
        /// <param name="token">The auth token</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpPost]
        public async Task<JsonResult> Login([FromForm] string token)
        {
            token = Uri.UnescapeDataString(token);
            await using var context = new Database();
            if (context.Auths.Any(f => f.Token == token && f.Type == Models.Database.Meta.AuthType.LOGIN))
            {
                var auth = await context.Auths.FirstAsync(f => f.Token == token);
                if (auth.IsValid)
                {
                    var user = await context.Accounts.Include(x => x.User).FirstAsync(f => f.Id == auth.AccountId);

                    //Important session variables
                    base.setUser(user);

                    return Json(new {status = "success", type = user.Type});
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