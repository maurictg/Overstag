using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;
using Overstag.Models.NoDB;

using Mollie.Api;
using Mollie.Api.Models;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;

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

        public async Task<IActionResult> Checkout()
        {
            try
            {
                string PayID = HttpContext.Session.GetString("PayID");

                if(string.IsNullOrEmpty(PayID))
                    return Content("Authenticatiefout!!!");
                //Get Invoice
                var invoice = new OverstagContext().Invoices.First(f => f.PayID == PayID);
                double amount = (double)invoice.Amount / 100;
                string cost = Math.Round(amount, 2).ToString("F").Replace(",",".");

                string url = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Done";
                string webhook = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Webhook";

                //Create payment
                PaymentClient pc = new PaymentClient("test_sapB3sVqDTfxCSjdutvaPyQRnrwUJQ");
                
                PaymentRequest pr = new PaymentRequest()
                {
                    Amount = new Amount(Currency.EUR, cost),
                    Description = $"Overstag factuur #{invoice.PayID}",
                    RedirectUrl = url,
                    //WebhookUrl = webhook,
                    Locale = "nl_NL",
                };

                pr.SetMetadata(invoice);

                PaymentResponse ps = await pc.CreatePaymentAsync(pr);
                Models.Payment p = new Models.Payment()
                {
                    InvoiceID = invoice.PayID,
                    PaymentID = ps.Id,
                    UserID = invoice.UserID
                };
                
                //Add p to database.
                //TODO:
                //Create webhook etc, check status e.d.

                return View();
            }
            catch { return Content("Er is iets fout gegaan!!!"); }
            
        }

        public async Task<IActionResult> Test()
        {
            Overstag.Payment.OPay o = new Payment.OPay();
            return Json(new { result = await o.Test() });
        }
    }
}
