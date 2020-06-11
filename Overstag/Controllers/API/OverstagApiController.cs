using Microsoft.AspNetCore.Mvc;
using Overstag.Authorization;
using Overstag.Models;

namespace Overstag.Controllers.API
{
    [OverstagAuthorize]
    public class OverstagApiController : Controller
    {
        protected int getUserId() => getUser().Id;
        protected Account getUser() => ((Account)HttpContext.Items["User"]);
        protected Auth getAuth() => ((Auth)HttpContext.Items["Auth"]);
    }
}
