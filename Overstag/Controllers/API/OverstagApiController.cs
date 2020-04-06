using Microsoft.AspNetCore.Mvc;

namespace Overstag.Controllers.API
{
    public class OverstagApiController : Controller
    {
        protected int getUserId() => ((Overstag.Models.Account)HttpContext.Items["User"]).Id;
    }
}
