using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using Overstag.Models.API;

namespace Overstag.Controllers.API
{
    [Route("api/user")]
    public class UserApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Details()
        {
            var current = (Account)HttpContext.Items["User"];
            return Json(new { status = "success", data = current.ToUserInfo() });
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            using (var context = new OverstagContext())
            {
                context.Auths.Remove((Auth)HttpContext.Items["Auth"]);
                await context.SaveChangesAsync();
            }

            return Json(new { status = "success" });
        }
    }
}
