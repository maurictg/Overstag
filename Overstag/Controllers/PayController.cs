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
using Mollie.Api.Models.Payment;

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
                    return Content("<h1 style=\"color: red;\">ERROR: InvoiceID in URL is wrong!!!. Invoice does not exist</h1>","text/html");
                else
                {
                    List<Event> events = new List<Event>();

                    foreach(string eve in invoice.EventIDs.Split(','))
                        events.Add(context.Events.First(f => f.Id == Convert.ToInt32(eve)));

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
                    return Content("<h1 style=\"color: red;\">Authenticatiefout!!!</h1>", "text/html");

                using (var context = new OverstagContext())
                {
                    //Get Invoice
                    var invoice = context.Invoices.First(f => f.PayID == PayID);
                    if (invoice.Payed == 1)
                        return Content("<h1 style=\"color: orange;\">Factuur is al betaald</h1>","text/html");


                    var user = context.Accounts.First(f => f.Id == invoice.UserID);

                    double amount = (double)invoice.Amount / 100;
                    string cost = Math.Round(amount, 2).ToString("F").Replace(",", ".");

                    string url = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Done/{Uri.EscapeDataString(invoice.PayID)}";
                    string webhook = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Webhook";

                    //Create payment
                    PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                    PaymentRequest pr = new PaymentRequest()
                    {
                        Amount = new Amount(Currency.EUR, cost),
                        Description = $"Overstag factuur #{invoice.Id}",
                        RedirectUrl = url,
                        //WebhookUrl = webhook,
                        Locale = "nl_NL",
                        CustomerId = user.MollieID
                    };

                    pr.SetMetadata(Uri.EscapeDataString(invoice.PayID));

                    PaymentResponse ps = await pc.CreatePaymentAsync(pr);
                   

                    context.Payments.Add(new Models.Payment(){
                        InvoiceID = invoice.PayID,
                        PaymentID = ps.Id,
                        UserID = invoice.UserID,
                        PlacedAt = DateTime.Now,
                        Status = ps.Status
                    });

                    context.SaveChanges();

                    string paylink = ps.Links.Checkout.Href;
                    return View("Checkout",paylink);
                }
            }
            catch { return Content("Er is iets fout gegaan!!!"); }
            
        }

        [HttpGet("Pay/Done/{id}")]
        public IActionResult Done(string id)
        { 
            using(var context = new OverstagContext())
            {
                var payment = context.Payments.First(f => f.InvoiceID == Uri.UnescapeDataString(id));
                return View("Done",payment.PaymentID);
            }
        }

        [HttpPost("Pay/Webhook")]
        public async Task<IActionResult> Webhook([FromHeader]string id)
        {
            await UpdatePayment(id);
            return Ok();
        }

        [HttpGet("Pay/UpdatePayment/{id}")]
        public async Task<IActionResult> UpdatePayment(string id)
        {
            try
            {
                PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                PaymentResponse ps = await pc.GetPaymentAsync(id);

                using (var context = new OverstagContext())
                {
                    var payment = context.Payments.FirstOrDefault(f => f.PaymentID == id);
                    if (payment != null)
                    {
                        payment.Status = ps.Status;
                        payment.PayedAt = ps.PaidAt;

                        /*
                        string iid = Uri.UnescapeDataString(ps.GetMetadata<string>());

                        var invoice = context.Invoices.First(f => f.PayID == iid);
                        if (payment.Status == PaymentStatus.Paid)
                            invoice.Payed = 1;
                        */
                        var invoice = context.Invoices.FirstOrDefault(f => f.PayID == payment.InvoiceID);
                        if (invoice != null)
                        {
                            if (payment.Status == PaymentStatus.Paid)
                                invoice.Payed = 1;

                            context.Invoices.Update(invoice);
                            context.Payments.Update(payment);
                            context.SaveChanges();
                        }
                        else
                            return Json(new { status = "warning", warning = "invoice not found in DB", ps });

                        return Json(new { status = "success", data = payment });
                    }
                    else
                        return Json(new { status = "warning", warning = "payment not found in DB", ps });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = e });
            }
        }

        [HttpGet("Pay/GetPayment/{id}")]
        public IActionResult GetPayment(string id)
        {
            using (var context = new OverstagContext())
            {
                var payment = context.Payments.First(f => f.PaymentID == id);
                return Json(new { status = "success", data = payment });
            }
        }


    }
}
