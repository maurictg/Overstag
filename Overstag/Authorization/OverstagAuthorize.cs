using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Overstag.Controllers;

namespace Overstag.Authorization
{
    public class OverstagAuthorize : Attribute, IAuthorizationFilter
    {
        private int[] _allowedRoles;

        /// <summary>
        /// Accept any logged in user 
        /// </summary>
        public OverstagAuthorize() => _allowedRoles = new[] {0, 1, 2, 3};

        /// <summary>
        /// Accept user with a specific role
        /// </summary>
        /// <param name="roles">allowed roles</param>
        public OverstagAuthorize(params int[] roles) => _allowedRoles = roles;

        /// <summary>
        /// Determines whether user has access to this action
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var user = OverstagController.GetCurrentUser(filterContext.HttpContext);
            if (user != null)
            {
                var userRole = user.Type;
                if (_allowedRoles.All(x => x != userRole)) filterContext.Result = new RedirectResult("/Error/403");
            }
            else filterContext.Result = new RedirectResult("/Error/401");
        }
    }
}