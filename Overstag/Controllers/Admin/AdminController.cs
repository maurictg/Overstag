using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Text.Json;
using Overstag.Authorization;
using Overstag.Models.Database;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        /*
        [OverstagAuthorize(3)]
        public IActionResult Index()
            => View();


        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>JSON</returns>
        [OverstagAuthorize(3)]
        public async Task<IActionResult> getUsers()
        {
            await using var context = new OverstagContext();
            return Json(await context.Accounts.OrderBy(f => f.Firstname).ToListAsync());
        }

        /// <summary>
        /// Login as a specified user
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <returns>Nothing, it redirects to /User</returns>
        [OverstagAuthorize(3)]
        [Route("Admin/loginAs/{token}")]
        public async Task<IActionResult> loginAs(string token)
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.FirstAsync(f => f.Token == Uri.UnescapeDataString(token));
            HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));
            HttpContext.Response.Redirect("/User");
            return Content("OK");
        }

        /// <summary>
        /// Update user type
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <param name="type">The new type</param>
        /// <returns>JSON(status=success)</returns>
        [OverstagAuthorize(3)]
        public async Task<IActionResult> saveType([FromQuery]int id, [FromQuery]byte type)
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.FindAsync(id);
            user.Type = type;
            context.Accounts.Update(user);
            context.SaveChanges();
            return Json(new { status = "success" });
        }

        */

        /// <summary>
        /// Auto generate database
        /// </summary>
        /// <returns>A response message</returns>
        [OverstagAuthorize(-1)]
        public async Task<IActionResult> InitDB()
        {
            try
            {
                await using var context = new Database();
                await context.Database.EnsureCreatedAsync();
                return Content("Database created!");
            }
            catch (Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
