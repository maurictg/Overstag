using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Overstag.Models;

namespace Overstag.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Authentication
    {
        private readonly RequestDelegate _next;
        //Paths that are allowed without login token
        private readonly string[] allowedpaths = { "/Home", "/Register", "/Admin/initdb", "/Pay" };

        public Authentication(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            bool allowed = false;
            if (path.HasValue)
            {

                foreach (string p in allowedpaths) { if (path.Value.ToString().StartsWith(p,StringComparison.CurrentCultureIgnoreCase)) { allowed = true; } }
                if (!allowed)
                {
                    if (httpContext.Session.GetString("Token") == null || httpContext.Session.GetString("Name") == null) //not logged in
                    {
                        httpContext.Response.Redirect("/Home");
                    }
                    else if(path.Value.ToString().StartsWith("/Admin", StringComparison.CurrentCultureIgnoreCase) && !httpContext.Session.GetString("Name").Equals("admin", StringComparison.CurrentCultureIgnoreCase))
                    {
                        httpContext.Response.Redirect("/Home");
                    }
                    else{ return _next(httpContext); }
                }
                else {return _next(httpContext); }
            }
            httpContext.Response.WriteAsync("Authentication error");
            return null;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Authentication>();
        }
    }
}
