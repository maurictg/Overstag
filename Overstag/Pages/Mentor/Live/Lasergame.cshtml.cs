using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Overstag.Controllers;

namespace Overstag
{
    public class LasergameModel : PageModel
    {
        public string Token { get; set; }

        public void OnGet()
        {
            try
            {
                this.Token = OverstagController.GetCurrentUser(HttpContext).Token;
            }
            catch
            {
                this.Token = null;
            }
        }
    }
}