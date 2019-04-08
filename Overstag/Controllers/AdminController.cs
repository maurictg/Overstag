using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Overstag.Controllers
{
    public class AdminController : Controller
    {
        public ContentResult Index()
        {
            return Content("<h1>Heey</h1>");
        }

        public string Init()
        {
            using(var context = new Overstag.Models.AccountContext())
            {
                try
                {
                    context.Database.EnsureCreatedAsync();
                    return "Calling: Admin/init.\n\nDatabase successfully restored!!";
                }catch(Exception ex)
                {
                    return "Calling: Admin/init.\n\nError: " + ex.ToString();
                }
            }
        }
    }
}