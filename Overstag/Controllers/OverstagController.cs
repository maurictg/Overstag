using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class OverstagController : Controller
    { 
        protected Account currentUser { get { return JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("CurrentUser")); } }
        protected bool isLoggedIn { get { return !string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")); } }
        protected void setUser(Account user) => HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));

        public static Account GetCurrentUser(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Session.GetString("CurrentUser")))
                return JsonSerializer.Deserialize<Account>(context.Session.GetString("CurrentUser"));
            else
                return null;
        }
    }
}
