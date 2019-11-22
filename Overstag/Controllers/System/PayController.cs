using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using System.Threading.Tasks;
using Overstag.Models.NoDB;
using Mollie.Api.Models;
using Mollie.Api.Client;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;
using Mollie.Api.Models.Payment;

namespace Overstag.Controllers
{
    public class PayController : Controller
    {
        /// <summary>
        /// Get an invoice by its token
        /// </summary>
        /// <param name="invoiceid">The URI-encoded invoice token</param>
        /// <returns>View with invoice and its details</returns>
        [Route("Pay/Direct/{invoiceid}")]
        [Route("Pay/Invoice/{invoiceid}")]
        public IActionResult Index(string invoiceid)
        {
            invoiceid = Uri.UnescapeDataString(invoiceid);
            using(var context = new OverstagContext())
            {
                var invoice = context.Invoices.FirstOrDefault(f => f.PayID == invoiceid);

                if (invoice == null)
                {
                    string[] error = { "Factuur niet gevonden", "Waarschijnlijk klopt de link niet. <br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                    return View("~/Views/Error/Custom.cshtml",error);
                }
                else
                {
                    List<Event> events = new List<Event>();

                    foreach(string eve in invoice.EventIDs.Split(','))
                        events.Add(context.Events.First(f => f.Id == Convert.ToInt32(eve)));

                    var iinvoice = new IInvoice {
                        Amount = invoice.Amount,
                        UserID = invoice.UserID,
                        Payed = (invoice.Payed==1),
                        PayID = invoice.PayID,
                        Timestamp = invoice.Timestamp,
                        Events = events.OrderBy(e => e.When).ToList(),
                        Additions = invoice.AdditionsCount
                    };

                    if(iinvoice.Payed)
                    {
                        //Validate payment
                        var payment = context.Payments.FirstOrDefault(f => f.InvoiceID == invoice.PayID);
                        if (payment != null && !string.IsNullOrEmpty(payment.PaymentID))
                            ViewBag.Payment = payment;
                    }

                    return View(new OPayInfo
                    {
                        User = context.Accounts.First(a => a.Id==invoice.UserID),
                        Invoice = iinvoice
                    });
                }
            }
        }

        /// <summary>
        /// Creates a Mollie payment
        /// </summary>
        /// <param name="invoiceid">The invoice token/invoiceID</param>
        /// <returns>View</returns>
        [HttpPost("Pay/Checkout")]
        public async Task<IActionResult> Checkout([FromForm]string invoiceid)
        {
            using (var context = new OverstagContext())
            {
                var invoice = context.Invoices.FirstOrDefault(f => f.PayID == Uri.UnescapeDataString(invoiceid));

                if(invoice == null)
                {
                    string[] error = { "Factuur niet gevonden", "Waarschijnlijk klopt de link niet. <br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }

                if(invoice.Payed == 1)
                {
                    string[] error = { "Factuur is al betaald", "<i>Wat probeer je? Ik zou echt werkelijk niet weten waarom je een factuur 2 keer zou betalen...</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }

                var user = context.Accounts.First(f => f.Id == invoice.UserID);

                string cost = Math.Round((double)invoice.Amount / 100, 2).ToString("F").Replace(",", ".");

                string url = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Done/{Uri.EscapeDataString(invoice.PayID)}";
                string webhook = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Webhook";

                //Create payment
                PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                PaymentRequest pr = new PaymentRequest()
                {
                    Amount = new Amount(Currency.EUR, cost),
                    Description = $"Overstag factuur #{invoice.Id}",
                    RedirectUrl = url,
                    Locale = "nl_NL",
                    CustomerId = user.MollieID,
                    #if !DEBUG
                    WebhookUrl = webhook
                    #endif
                };


                pr.SetMetadata(Uri.EscapeDataString(invoice.PayID));
                //PaymentResponse ps = await pc.CreatePaymentAsync(pr); //<--temp

                context.Payments.Add(new Payment()
                {
                    InvoiceID = invoice.PayID,
                    //PaymentID = ps.Id, //<--temp
                    UserID = invoice.UserID,
                    PlacedAt = DateTime.Now,
                    //Status = ps.Status //<--temp
                });

                //ViewBag.PayLink = ps.Links.Checkout.Href; //<--temp
                await context.SaveChangesAsync();

                //Viewbag info
                ViewBag.Amount = invoice.Amount;

                return View(context.Payments.First(f => f.InvoiceID == invoice.PayID));
            }
        }

        /// <summary>
        /// Cancel and delete payment by invoice ID
        /// </summary>
        /// <param name="payid">The paymentid</param>
        /// <returns></returns>
        [HttpPost("Pay/Cancel")]
        public async Task<IActionResult> CancelPayment([FromForm]int id)
        {
            using(var context = new OverstagContext())
            {
                var payment = context.Payments.FirstOrDefault(f => f.Id == id);
                if (payment == null)
                    return Json(new { status = "error", error = "Betaling niet gevonden!" });

                if (payment.Status == PaymentStatus.Paid)
                    return Json(new { status = "payed", error = "Betaling is al betaald." });

                context.Payments.Remove(payment);

                if(!string.IsNullOrEmpty(payment.PaymentID))
                {
                    try
                    {
                        PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                        await pc.DeletePaymentAsync(payment.PaymentID);
                    }
                    catch(Exception e)
                    {
                        return Json(new { status = "warning", error = "IDeal betaling verwijderen mislukt.", debuginfo = e.Message });
                    }
                }

                try
                {
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden.", debuginfo = e.Message });
                }
            }
        }

        /// <summary>
        /// Redirect page for Mollie
        /// </summary>
        /// <param name="id">The invoice's token or invoiceID</param>
        /// <returns>View</returns>
        [HttpGet("Pay/Done/{id}")]
        public IActionResult Done(string id)
        { 
            using(var context = new OverstagContext())
            {
                var payment = context.Payments.FirstOrDefault(f => f.InvoiceID == Uri.UnescapeDataString(id));
                if (payment == null)
                {
                    string[] error = { "Betaling niet gevonden", "Waarschijnlijk is de URL onjuist.<br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }

                return View("Done",payment.PaymentID);
            }
        }

        /// <summary>
        /// Webhook for mollie
        /// </summary>
        /// <param name="id">The mollieID</param>
        /// <returns>HTTP status 200 (OK)</returns>
        [HttpPost("Pay/Webhook")]
        public async Task<IActionResult> Webhook([FromForm]string id)
        {
            await UpdatePayment(id);
            return Ok();
        }

        /// <summary>
        /// Update payment using MollieAPI
        /// </summary>
        /// <param name="id">The mollie paymentID</param>
        /// <returns>JSON (status = success with payment data, status = warning with warning, status = error with error)</returns>
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
                            {
                                invoice.Payed = 1;
                                context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[IDEAL] Betaling (#{payment.PaymentID}) van factuur door {context.Accounts.First(f => f.Id == payment.UserID).Firstname}", When = DateTime.Now });
                            }
                            
                            if(payment.Status != PaymentStatus.Open)
                                HttpContext.Session.Remove("PayUrl");

                            context.Invoices.Update(invoice);
                            context.Payments.Update(payment);
                            context.SaveChanges();
                        }
                        else
                            return Json(new { status = "warning", warning = "Factuur niet gevonden in de database", ps });

                        return Json(new { status = "success", data = payment });
                    }
                    else
                        return Json(new { status = "warning", warning = "Betaling niet gevonden in de database", ps });
                }
            }
            catch (Exception e)
            {
                return Json(new { status = "error", error = e.ToString() });
            }
        }

        /// <summary>
        /// Get payment status and details from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON (status = success with data or status = error with details)</returns>
        [HttpGet("Pay/GetPayment/{id}")]
        public IActionResult GetPayment(string id)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var payment = context.Payments.First(f => f.PaymentID == id);
                    payment.InvoiceID = Uri.EscapeDataString(payment.InvoiceID);
                    return Json(new { status = "success", data = payment });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Er is intern iets fout gegaan", debuginfo = e });
            }
        }


    }
}
