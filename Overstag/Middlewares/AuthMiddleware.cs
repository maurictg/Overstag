using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Overstag.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Authentication
    {
        private readonly RequestDelegate _next;

        private readonly Dictionary<int, string> allowedpaths = new Dictionary<int, string>()
        {
            {0, "/User,/Files" },
            {1, "/Parent" },
            {2, "/Mentor,/Accountancy" },
            {3, "/Admin" }
        };

        private readonly string[] allpaths =
        {
            "/Home", "/Register", "/Admin/initdb", "/Pay", "/Auth", "/User", "/Photo", "/Files", "/Parent", "/Mentor", "/Admin", "/Accountancy", "/ws", "/Public"
        };

        public Authentication(RequestDelegate next)
            => _next = next;

        public Task Invoke(HttpContext c)
        {
            if (c.Request.Path.HasValue)
            {
                string p = c.Request.Path.Value;
                int? type = c.Session.GetInt32("Type");
                int code = 400;
                bool loggedin = (type != null);

                List<string> allowed = new List<string>() { "/Home", "/Register", "/Admin/initdb", "/Pay", "/Auth", "/Photo", "/ws", "/Public" };

                if (loggedin)
                    for (int i = 0; i <= type; i++)
                        allowed.AddRange(allowedpaths[i].Split(','));

                bool containsall = false;
                bool containsallowed = false;

                foreach (string path in allpaths)
                    if (p.ToLower().StartsWith(path.ToLower()))
                        containsall = true;

                foreach (string path in allowed)
                    if (p.ToLower().StartsWith(path.ToLower()))
                        containsallowed = true;

                if (containsall)
                {
                    if (!containsallowed)
                        code = (loggedin) ? 403 : 401;
                    else
                        code = -1;
                }
                else
                {
                    code = -1;
                }

                if (code != -1)
                    c.Response.Redirect("/Error/"+code.ToString());
                else
                    return _next(c);
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
