using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;

namespace Overstag.Authorization
{
    public class ApiAuthorize : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Determines whether api has access to this action
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            /*var token = filterContext.HttpContext.Request.Headers["Token"].ToString();
            if (token == null) filterContext.Result = new JsonResult(new {status = "error", error = "No token provided"});
            else
            {
                var auth = new OverstagContext().Auths
                    .Include(f => f.User)
                    .FirstOrDefault(g => g.IP == "OVERSTAG_APP" && g.Token == token);

                if (auth == null) filterContext.Result = new JsonResult(new {status = "error", error = "Invalid token"});
                else
                {
                    filterContext.HttpContext.Items.Add("User", auth.User);
                    filterContext.HttpContext.Items.Add("Auth", auth);
                }
            }*/
        }
    }
}