using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Overstag.Models;
using Overstag.Models.API;
using Newtonsoft.Json;
using System.Collections;

namespace Overstag.Controllers.API
{
    [Route("api/invoices")]
    public class InvoicesApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery]bool hidePayed)
        {
            //Get invoices
            List<Invoice> invoices = await new OverstagContext().Invoices.Include(g => g.Payment).Where(f => f.UserID == getUserId()).ToListAsync();
            int count = (hidePayed) ? invoices.Count(f => !f.Payed) : invoices.Count();
            List<InvoiceInfo> inv = invoices.Select(h => h.toInvoiceInfo(string.Format("{0}://{1}/Pay/Invoice/", HttpContext.Request.Scheme, HttpContext.Request.Host))).ToList();

            return Json(new { status = "success", count, invoices = (hidePayed) ? inv.Where(f => !f.Payed).ToList() : inv });
        }
    }
}
