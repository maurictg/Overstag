#define MOLLIE_ENABLED

//COMMENT this to enable mollie
#undef MOLLIE_ENABLED
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
using Microsoft.EntityFrameworkCore;

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
                var invoice = context.Invoices.FirstOrDefault(f => f.InvoiceID == invoiceid);

                if (invoice == null)
                {
                    string[] error = { "Factuur niet gevonden", "Waarschijnlijk klopt de link niet. <br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                    return View("~/Views/Error/Custom.cshtml",error);
                }
                else
                {
                    if(invoice.Payed)
                    {
                        //Validate payment
                        var payment = context.Payments.Include(f => f.Invoice).OrderByDescending(f => f.PlacedAt).FirstOrDefault(f => f.Invoice.InvoiceID == invoice.InvoiceID);
                        if (payment != null && !string.IsNullOrEmpty(payment.PaymentID))
                            ViewBag.Payment = payment;
                    }

                    return View(Services.Invoices.GetXInvoice(invoice.Id));
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
            bool createPayment = true;

            using (var context = new OverstagContext())
            {
                var invoice = await context.Invoices.Include(x => x.User).Include(y => y.Payment).FirstOrDefaultAsync(f => f.InvoiceID == Uri.UnescapeDataString(invoiceid));

                if(invoice == null)
                {
                    string[] error = { "Factuur niet gevonden", "Waarschijnlijk klopt de link niet. <br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }


                if(invoice.Payment != null)
                {
                    bool save = false;
                    if (invoice.Payment.Status == PaymentStatus.Paid)
                    {
                        save = true;
                        invoice.Payed = true;
                    }
                    else if(invoice.Payment.Status == PaymentStatus.Canceled || invoice.Payment.Status == PaymentStatus.Expired || invoice.Payment.Status == PaymentStatus.Failed)
                    {
                        save = true;
                        invoice.Payment = null;
                    }
                    else if (invoice.Payment.Status == null)
                        createPayment = false;

                    try
                    {
                        if (save)
                        {
                            context.Invoices.Update(invoice);
                            await context.SaveChangesAsync();
                        }
                    }
                    catch(Exception e) { throw e; }
                }

                if (invoice.Payed)
                {
                    string[] error = { "Factuur is al betaald", "<i>Wat probeer je? Ik zou echt werkelijk niet weten waarom je een factuur 2 keer zou betalen...</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }

                //Viewbag info
                ViewBag.Amount = invoice.Amount;

                if (!createPayment)
                    return View(invoice.Payment);


                var user = invoice.User;
                string cost = Math.Round((double)invoice.Amount / 100, 2).ToString("F").Replace(",", ".");

                string url = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Done/{Uri.EscapeDataString(invoice.InvoiceID)}";
                string webhook = $"{string.Format("{0}://{1}", HttpContext.Request.Scheme, HttpContext.Request.Host)}/Pay/Webhook";

                //Create payment
                PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);               

                IdealPaymentRequest pr = new IdealPaymentRequest()
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

                pr.SetMetadata(Uri.EscapeDataString(invoice.InvoiceID));
#if MOLLIE_ENABLED
                PaymentResponse ps = await pc.CreatePaymentAsync(pr);
                ViewBag.PayLink = ps.Links.Checkout.Href;
#endif
                context.Payments.Add(new Payment()
                {
                    Invoice = invoice,
                    User = invoice.User,
                    PlacedAt = DateTime.Now,
#if MOLLIE_ENABLED
                    PaymentID = ps.Id,
                    Status = ps.Status
#endif
                });

                await context.SaveChangesAsync();

                return View(context.Payments.Include(x => x.Invoice).First(f => f.Invoice.InvoiceID == invoice.InvoiceID));
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

                /*if(!string.IsNullOrEmpty(payment.PaymentID))
                {
                    try
                    {
#if MOLLIE_ENABLED
                        PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);

                        var t = await pc.GetPaymentAsync(payment.PaymentID);
                        if(t.IsCancelable)
                            await pc.DeletePaymentAsync(payment.PaymentID);
                        else
                            return Json(new { status = "warning", error = "Betaling kan niet verwijderd worden." });
#endif
                    }
                    catch (Exception e)
                    {
                        return Json(new { status = "warning", error = "IDeal betaling verwijderen mislukt.", debuginfo = e.Message });
                    }
                }*/

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
                var payment = context.Payments.Include(x => x.Invoice).OrderByDescending(f => f.PlacedAt).FirstOrDefault(f => f.Invoice.InvoiceID == Uri.UnescapeDataString(id));
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
                    var payment = context.Payments.Include(x => x.Invoice).Include(y => y.User).FirstOrDefault(f => f.PaymentID == id);
                    if (payment != null)
                    {
                        payment.Status = ps.Status;
                        payment.PayedAt = ps.PaidAt;

                        var invoice = payment.Invoice;
                        if (invoice != null)
                        {
                            if (payment.Status == PaymentStatus.Paid)
                            {
                                invoice.Payed = true;
                                context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[IDEAL] Betaling (#{payment.PaymentID}) van factuur door {payment.User.Firstname}", When = DateTime.Now, Type = 1, Payed = true, UserId = payment.User.Id });
                            }
                            
                            //if(payment.Status != PaymentStatus.Open)
                            //HttpContext.Session.Remove("PayUrl");

                            context.Invoices.Update(invoice);
                            context.Payments.Update(payment);
                            await context.SaveChangesAsync();
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
                    var payment = context.Payments.Include(f => f.Invoice).First(f => f.PaymentID == id);
                    payment.Invoice.InvoiceID = Uri.EscapeDataString(payment.Invoice.InvoiceID);
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
