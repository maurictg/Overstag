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

        public async Task<IActionResult> getUsers()
        {
            await using var context = new OverstagContext();
            return Json(await context.Accounts.OrderBy(f => f.Firstname).ToListAsync());
        }

        [Route("Admin/loginAs/{token}")]
        public async Task<IActionResult> loginAs(string token)
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.FirstAsync(f => f.Token == Uri.UnescapeDataString(token));
            HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));
            HttpContext.Response.Redirect("/User");
            return Content("OK");
        }

        public async Task<IActionResult> saveType([FromQuery]int id, [FromQuery]byte type)
        {
            await using var context = new OverstagContext();
            var user = await context.Accounts.FindAsync(id);
            user.Type = type;
            context.Accounts.Update(user);
            context.SaveChanges();
            return Json(new { status = "success" });
        }

        public async Task<IActionResult> InitDB()
        {
            try
            {
                await using var context = new OverstagContext();
                await context.Database.EnsureCreatedAsync();
                return Content("Database created!");
            }catch(Exception e)
            {
                return Content(e.ToString());
            }
        }
    }
}
