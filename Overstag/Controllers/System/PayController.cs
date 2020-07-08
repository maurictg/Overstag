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
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
        public async Task<IActionResult> Index(string invoiceid, [FromQuery]bool showPayment)
        {
            invoiceid = Uri.UnescapeDataString(invoiceid);
            await using var context = new OverstagContext();
            var invoice = await context.Invoices.FirstOrDefaultAsync(f => f.InvoiceID == invoiceid);
            
            if (invoice == null)
            {
                string[] error = { "Factuur niet gevonden", "Waarschijnlijk klopt de link niet. <br/><i>Als het probleem blijft optreden neem dan contact met ons op.</i>" };
                return View("~/Views/Error/Custom.cshtml",error);
            }
            else
            {
                if (invoice.UserID == null)
                {
                    string[] error = { "Account verwijderd", "De gebruiker van wie deze factuur was heeft zijn of haar account verwijderd. <br/><i>Als u de data nodig heeft kunt u nog de printversie bij ons opvragen.</i>" };
                    return View("~/Views/Error/Custom.cshtml", error);
                }

                if (invoice.Paid || showPayment)
                {
                    //Validate payment
                    var payment = await context.Payments.Include(f => f.Invoice).OrderByDescending(f => f.PlacedAt).FirstOrDefaultAsync(f => f.Invoice.InvoiceID == invoice.InvoiceID);
                    if (payment != null)
                        ViewBag.Payment = payment;
                }

                if (showPayment)
                    ViewBag.showPayment = true;

#if MOLLIE_ENABLED
                    ViewBag.MollieEnabled = true;
#endif
                
                return View(Services.Invoices.GetXInvoice(invoice.Id));
            }
        }

        /// <summary>
        /// Set the meta data of an payment
        /// </summary>
        /// <param name="metadata">A string, JSON</param>
        /// <param name="id">The payment its ID</param>
        /// <returns>JSON, status</returns>
        [HttpPost]
        public async Task<JsonResult> SetMetaData([FromForm]string metadata, [FromForm]int id)
        {
            await using var context = new OverstagContext();
            var payment = await context.Payments.FindAsync(id);
            if (payment == null)
                return Json(new { status = "error", error = "Betaling niet gevonden" });

            payment.Metadata = metadata;
            context.Payments.Update(payment);
            await context.SaveChangesAsync();
            return Json(new { status = "success" });
        }

        /// <summary>
        /// Creates a payment
        /// </summary>
        /// <param name="invoiceId">The token of an invoice</param>
        /// <param name="type">The payment type</param>
        /// <returns>View</returns>
        [HttpPost("Pay/Checkout/{invoiceId}")]
        public async Task<IActionResult> Checkout(string invoiceId, [FromForm]int type)
        {
            await using var context = new OverstagContext();
            var invoice = await context.Invoices.Include(a => a.Payment).Include(b => b.User).FirstOrDefaultAsync(f => f.InvoiceID == Uri.UnescapeDataString(invoiceId));
            var paymentType = (PayType)type;

            if (invoice == null)
                return View("~/Views/Error/Custom.cshtml", new string[] { "Factuur niet gevonden", "De factuur die u probeert te betalen is niet gevonden. Probeer het opnieuw, en als het niet lukt neem dan contact met ons op" });

            ViewBag.Amount = invoice.Amount;

            Payment p = new Payment()
            {
                InvoiceId = invoice.Id,
                PayType = paymentType,
                PlacedAt = DateTime.Now,
                UserId = (int)invoice.UserID
            };

            if (invoice.Payment != null)
            {
                if (invoice.Payment.IsPaid())
                {
                    invoice.Paid = true;
                    context.Invoices.Update(invoice);
                    await context.SaveChangesAsync();
                    return View("~/Views/Error/Custom.cshtml", new string[] { "Factuur is al betaald.", "De factuur die u probeert te betalen is al betaald." });
                }

                if(!invoice.Payment.IsFailed())
                {
                    p = invoice.Payment;

#if MOLLIE_ENABLED
                        if(!p.IsPaid() && p.PayType == PayType.MOLLIE)
                        {
                            //Get url
                            PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                            PaymentResponse ps = await pc.GetPaymentAsync(p.PaymentId);
                            if (ps.Links.Checkout != null) //Still open for payment
                            {
                                ViewBag.PayLink = ps.Links.Checkout.Href;
                            }
                        }
#endif

                    return View(p);
                }
                else
                {
                    //Remove old payment
                    context.Payments.Remove(invoice.Payment);
                }
            }


            switch (paymentType)
            {
                case PayType.MOLLIE:
                {
                    string cost = Math.Round((double)invoice.Amount / 100, 2).ToString("F").Replace(",", ".");
                    string redirect = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Pay/Invoice/{Uri.EscapeDataString(invoice.InvoiceID)}?showPayment=true";
                    string webhook = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/Pay/Webhook";

                    PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                    IdealPaymentRequest pr = new IdealPaymentRequest()
                    {
                        Amount = new Amount(Currency.EUR, cost),
                        Description = $"Overstag factuur #{invoice.GetInvoiceNumber()}",
                        RedirectUrl = redirect,
                        Locale = "nl_NL",
                        CustomerId = invoice.User.MollieID,
                        Metadata = Uri.EscapeDataString(invoice.InvoiceID),
#if !DEBUG
                                WebhookUrl = webhook
#endif
                    };

#if MOLLIE_ENABLED
                            PaymentResponse ps = await pc.CreatePaymentAsync(pr);
                            ViewBag.PayLink = ps.Links.Checkout.Href;
                            p.PaymentId = ps.Id;
                            p.Status = ps.Status;
#endif
                }
                    break;
                case PayType.BANK:
                    //Create payment request via bank or tikkie
                    p.Status = PaymentStatus.Open;
                    break;
                default:
                    return View("~/Views/Error/Custom.cshtml", new[] { "Betaalmethode niet ondersteund", "De betaalmethode die u heeft gekozen wordt niet ondersteund." });
            }

            context.Payments.Add(p);
            await context.SaveChangesAsync();

            return View(await context.Payments.Include(x => x.Invoice).FirstAsync(f => f.Invoice.InvoiceID == invoice.InvoiceID));
        }

        /// <summary>
        /// Cancel and delete payment by its id
        /// </summary>
        /// <param name="id">The payment its id</param>
        /// <returns>JSON</returns>
        [HttpPost("Pay/Cancel")]
        public async Task<IActionResult> CancelPayment([FromForm]int id)
        {
            await using var context = new OverstagContext();
            var payment = await context.Payments.FirstOrDefaultAsync(f => f.Id == id);
            if (payment == null)
                return Json(new { status = "error", error = "Betaling niet gevonden!" });

            if (payment.Status == PaymentStatus.Paid)
                return Json(new { status = "Paid", error = "Betaling is al betaald." });

            context.Payments.Remove(payment);

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

                await using var context = new OverstagContext();
                var payment = await context.Payments.Include(x => x.Invoice).Include(y => y.User).FirstOrDefaultAsync(f => f.PaymentId == id);
                if (payment != null)
                {
                    payment.Status = ps.Status;
                    payment.PaidAt = ps.PaidAt;

                    var invoice = payment.Invoice;
                    if (invoice != null)
                    {
                        if (payment.Status == PaymentStatus.Paid)
                        {
                            invoice.Paid = true;
                            context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[IDEAL] Betaling (#{payment.PaymentId}) van factuur door {payment.User.Firstname}", When = DateTime.Now, Type = 1, Paid = true, UserId = payment.User.Id });
                        }

                        string json = JsonConvert.SerializeObject(
                            new { 
                                klantNr = (ps.CustomerId ?? "-"), aanvraagKosten = ((ps.ApplicationFee != null) ? ps.ApplicationFee.Amount.ToString() : "-"), 
                                betaaldOp = ((ps.PaidAt == null) ? "-" : ((DateTime)ps.PaidAt).ToString("dd-MM-yyyy 'om' HH:mm:ss")),  
                                betaalMeth = ((ps.Method != null) ? ps.Method.Value.ToString() : "?"),
                                orderNr = (ps.OrderId ?? "-"),
                                kosten = (ps.Amount == null) ? "?" : ps.Amount.ToString(),
                                kostenRest = (ps.AmountRemaining == null) ? "?" : ps.AmountRemaining.ToString(),
                                omschrijving = ps.Description
                            }, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                        payment.Metadata = json;

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
        public async Task<IActionResult> GetPayment(int id, [FromQuery]bool htmlOnly, [FromQuery]bool force)
        {
            try
            {
                await using var context = new OverstagContext();
                var payment = await context.Payments.Include(f => f.Invoice).FirstAsync(f => f.Id == id);
                payment.Invoice.InvoiceID = Uri.EscapeDataString(payment.Invoice.InvoiceID);

                if(force && payment.PayType == PayType.MOLLIE)
                {
                    await UpdatePayment(payment.PaymentId);
                }

                if(htmlOnly)
                    return Json(new { status = "success", html = Services.Html.PayStatus(payment.Status), payStatus = (int)payment.Status });
                else
                    return Json(new { status = "success", data = payment, html = Services.Html.PayStatus(payment.Status), payStatus = (int)payment.Status });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Er is intern iets fout gegaan", debuginfo = e });
            }
        }


    }
}
