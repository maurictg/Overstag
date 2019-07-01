using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using Overstag.Models.NoDB;

namespace Overstag.Controllers
{
    public class PayController : Controller
    {
        [Route("/Pay/Direct/{invoiceid}")]
        public IActionResult Index(string invoiceid)
        {
            invoiceid = Uri.UnescapeDataString(invoiceid);
            using(var context = new OverstagContext())
            {
                var invoice = context.Invoices.FirstOrDefault(f => f.PayID == invoiceid);

                if (invoice == null)
                    return Content("ERROR: InvoiceID in URL is wrong!!!. Invoice does not exist");
                else
                {
                    List<Event> events = new List<Event>();

                    foreach(string eve in invoice.EventIDs.Split(','))
                    {
                        events.Add(context.Events.First(f => f.Id == Convert.ToInt32(eve)));
                    }

                    var iinvoice = new IInvoice {
                        Amount = invoice.Amount,
                        UserID = invoice.UserID,
                        Payed = invoice.Payed==1,
                        PayID = invoice.PayID,
                        Timestamp = invoice.Timestamp,
                        Events = events.OrderBy(e => e.When).ToList()
                    };

                    HttpContext.Session.SetString("PayID", invoice.PayID);

                    return View(new OPayInfo
                    {
                        User = context.Accounts.First(a => a.Id==invoice.UserID),
                        Invoice = iinvoice
                    });
                }
            }
        }

        public IActionResult Payment()
        {
            try
            {
                string PayID = HttpContext.Session.GetString("PayID");

                if(string.IsNullOrEmpty(PayID))
                    return Content("Authenticatiefout!!!");

                return View("Payment",PayID);
            }
            catch { return Content("Authenticatiefout!!!"); }
            
        }
    }
}
