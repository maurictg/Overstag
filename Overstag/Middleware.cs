using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Overstag.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Authentication
    {
        private readonly RequestDelegate _next;
        //Allowed paths are defined per user type
        private readonly string[] typenull = { "/Home", "/Register", "/Admin/initdb", "/Pay", "/html","/API" }
        ,typezero = { "/User", "/Photo", "/Tickets" }, typeone = { "/Parent" } ,typetwo = { "/Mentor" } ,typethree = { "/Admin" };

        public Authentication(RequestDelegate next)
            => _next = next;

        public Task Invoke(HttpContext c)
        {
            if (c.Request.Path.HasValue)
            {
                string p = c.Request.Path.Value;
                int? t = c.Session.GetInt32("Type");
                bool allowed = false;

                //Calculate list with allowed paths
                List<string> paths = new List<string>();
                paths.AddRange(typenull);

                if(t != null)
                {
                    if (t >= 3)
                        paths.AddRange(typethree);

                    if (t >= 2)
                        paths.AddRange(typetwo);

                    if (t >= 1)
                        paths.AddRange(typeone);

                    if (t >= 0)
                        paths.AddRange(typezero);
                }

                foreach (string path in paths)
                    if (p.StartsWith(path, StringComparison.CurrentCultureIgnoreCase))
                        allowed = true;

                if(allowed)
                    return _next(c);

                c.Response.Redirect("/Home");
            }

            c.Response.WriteAsync("Authentication error");
            return null;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
            => builder.UseMiddleware<Authentication>();
    }
}
