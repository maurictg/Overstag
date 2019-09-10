using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;

namespace Overstag.Controllers
{
    public class WWWController : Controller
    {
        [Route("html/{file}")]
        public IActionResult ServeHTM(string file)
        {
            string path = Path.Combine(new ApplicationEnvironment().ApplicationBasePath, "www", "html", file);
            if (!System.IO.File.Exists(path))
                return NotFound();
            else
                return File(System.IO.File.ReadAllBytes(path), "text/html");
        }
    }
}
