using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overstag.Controllers
{
    public class TestController : Controller
    {
        [HttpPost]
        public JsonResult TestCallback([FromQuery]string id, [FromForm]string apiKey)
        {
            return Json(new { id, apiKey });
        }
    }
}
