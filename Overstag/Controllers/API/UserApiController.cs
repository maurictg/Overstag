using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;

namespace Overstag.Controllers.API
{
    /*
    [Route("api/user")]
    public class UserApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public IActionResult Details()
            => Json(new { status = "success", data = ((Account)HttpContext.Items["User"]).ToUserInfo() });

        [HttpGet]
        [Route("auth")]
        public IActionResult GetAuthToken() 
            => Json(new { status = "success", auth = (Auth)HttpContext.Items["Auth"]});

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await using var context = new OverstagContext();
            context.Auths.Remove((Auth)HttpContext.Items["Auth"]);
            await context.SaveChangesAsync();
            return Json(new { status = "success" });
        }
    }*/
}
