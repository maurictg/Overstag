using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Overstag
{
    public class LasergameModel : PageModel
    {
        public string Token { get; set; }

        public void OnGet()
        {
            this.Token = HttpContext.Session.GetString("Token");
        }
    }
}