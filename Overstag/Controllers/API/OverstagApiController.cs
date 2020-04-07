using Microsoft.AspNetCore.Mvc;
using Overstag.Models;

namespace Overstag.Controllers.API
{
    public class OverstagApiController : Controller
    {
        protected int getUserId() => getUser().Id;
        protected Account getUser() => ((Account)HttpContext.Items["User"]);
        protected Auth getAuth() => ((Auth)HttpContext.Items["Auth"]);
    }
}
