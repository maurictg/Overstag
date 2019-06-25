using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using Overstag.Models.NoDB;

namespace Overstag.Controllers
{
    public class PayController : Controller
    {
        [Route("/Pay/Direct/{invoiceid}")]
        public IActionResult DirectPay(string invoiceid)
        {
            using(var context = new OverstagContext())
            {
                var invoice = context.Invoices.FirstOrDefault(f => f.PayID == invoiceid);
                if (invoice == null)
                    return Content("ERROR: InvoiceID in URL is wrong!!!. Invoice does not exist");
                else
                    return Content(invoiceid);
            }
        }
    }
}
