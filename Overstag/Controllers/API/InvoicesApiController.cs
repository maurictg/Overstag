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
using Overstag.Authorization;

namespace Overstag.Controllers.API
{
    [OverstagAuthorize]
    [Route("api/invoices")]
    public class InvoicesApiController : OverstagApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> List([FromQuery]bool hidePaid)
        {
            //Get invoices
            List<Invoice> invoices = await new OverstagContext().Invoices.Include(g => g.Payment).Where(f => f.UserID == getUserId()).ToListAsync();
            int count = (hidePaid) ? invoices.Count(f => !f.Paid) : invoices.Count();
            List<InvoiceInfo> inv = invoices.Select(h => h.toInvoiceInfo(string.Format("{0}://{1}/Pay/Invoice/", HttpContext.Request.Scheme, HttpContext.Request.Host))).ToList();

            return Json(new { status = "success", count, invoices = (hidePaid) ? inv.Where(f => !f.Paid).ToList() : inv });
        }

        [HttpGet]
        [Route("{invoiceId}")]
        public async Task<IActionResult> GetById(string invoiceId)
        {
            //Get invoices
            var invoice = await new OverstagContext().Invoices.Include(f => f.Payment).FirstOrDefaultAsync(x => x.InvoiceID == Uri.UnescapeDataString(invoiceId));
            if (invoice == null)
                return Json(new { status = "error", error = "Invoice not found" });
            else
                return Json(new { status = "success", invoice = invoice.toInvoiceInfo(string.Format("{0}://{1}/Pay/Invoice/", HttpContext.Request.Scheme, HttpContext.Request.Host)) });
        }
    }
}
