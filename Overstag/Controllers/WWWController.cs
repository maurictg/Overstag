using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.PlatformAbstractions;
using Overstag.Classes.Cryptography;
using Overstag.Models;
using System.IO;
using System.Security.Cryptography;

namespace Overstag.Controllers
{
    public class WWWController : Controller
    {
        /// <summary>
        /// Serve a html file by name
        /// </summary>
        /// <param name="file">The file's name</param>
        /// <returns>File or error 404</returns>
        
        [Route("html/{file}")]
        public IActionResult Serve(string file)
        {
            string path = Path.Combine(new ApplicationEnvironment().ApplicationBasePath, "www", "html", file);
            if (!System.IO.File.Exists(path))
                return NotFound();
            else
                return File(System.IO.File.ReadAllBytes(path), "text/html");
        }

    }
}
