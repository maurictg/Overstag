using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class OverstagController : Controller
    { 
        protected Account currentUser => JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("CurrentUser"));
        protected bool isLoggedIn => !string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser"));
        protected void setUser(Account user) => HttpContext.Session.Set("CurrentUser", JsonSerializer.SerializeToUtf8Bytes(user));

        /// <summary>
        /// Get current user from session
        /// </summary>
        /// <param name="context">Httpcontext with session in it</param>
        /// <returns>Account</returns>
        public static Account GetCurrentUser(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Session.GetString("CurrentUser")))
                return JsonSerializer.Deserialize<Account>(context.Session.Get("CurrentUser"));
            else return null;
        }
    }
}
