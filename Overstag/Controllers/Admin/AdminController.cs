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

namespace Overstag.Controllers
{
    [OverstagAuthorize(3)]
    public class AdminController : Controller
    {
        public IActionResult Index()
            => View();

        /// <summary>
        /// Get all users from the database
        /// </summary>
        /// <returns>JSON</returns>
        public IActionResult getUsers()
            => Json(new OverstagContext().Accounts.OrderBy(f => f.Firstname).ToList());

        /// <summary>
        /// Login as a specified user
        /// </summary>
        /// <param name="token">The user's token</param>
        /// <returns>Nothing, it redirects to /User</returns>
        [Route("Admin/loginAs/{token}")]
        public IActionResult loginAs(string token)
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.First(f => f.Token == Uri.UnescapeDataString(token));
                HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));
                HttpContext.Response.Redirect("/User");
                return Content("OK");
            }
        }

        /// <summary>
        /// Update user type
        /// </summary>
        /// <param name="id">The user's id</param>
        /// <param name="type">The new type</param>
        /// <returns>JSON(status=success)</returns>
        public IActionResult saveType([FromQuery]int id, [FromQuery]byte type)
        {
            using(var context = new OverstagContext())
            {
                var user = context.Accounts.Find(id);
                user.Type = type;
                context.Accounts.Update(user);
                context.SaveChanges();
                return Json(new { status = "success" });
            }
        }

        /// <summary>
        /// Auto generate database
        /// </summary>
        /// <returns>A response message</returns>
        public IActionResult InitDB()
        {
            try
            {
                new OverstagContext().Database.EnsureCreated();
                return Content("Database created!");
            }catch(Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
