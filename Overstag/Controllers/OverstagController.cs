using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class OverstagController : Controller
    { 
        /// <summary>
        /// Get the account of the user that is logged in
        /// </summary>
        protected Account currentUser { get { return JsonSerializer.Deserialize<Account>(HttpContext.Session.GetString("CurrentUser")); } }

        /// <summary>
        /// Validate if the user is logged in
        /// </summary>
        protected bool isLoggedIn { get { return !string.IsNullOrEmpty(HttpContext.Session.GetString("CurrentUser")); } }
        protected void setUser(Account user) => HttpContext.Session.SetString("CurrentUser", JsonSerializer.Serialize(user));

        /// <summary>
        /// Get current user from session
        /// </summary>
        /// <param name="context">Httpcontext with session in it</param>
        /// <returns>Account</returns>
        public static Account GetCurrentUser(HttpContext context)
        {
            if (!string.IsNullOrEmpty(context.Session.GetString("CurrentUser")))
                return JsonSerializer.Deserialize<Account>(context.Session.GetString("CurrentUser"));
            else
                return null;
        }
    }
}
