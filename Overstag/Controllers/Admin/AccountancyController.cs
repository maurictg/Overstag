using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using Overstag.Models.NoDB;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Controllers
{
    public class AccountancyController : Controller
    {
        [Route("Accountancy/Index/{amount?}")]
        public IActionResult Index(int? amount)
        {
            using(var context = new OverstagContext())
            {
                var pms = new List<Accountancy.Transaction>();
                if(amount != null)
                {
                    pms = context.Transactions.OrderByDescending(f => f.When).Take(Convert.ToInt32(amount)).ToList();
                    ViewBag.Limit = Convert.ToInt32(amount);
                    ViewBag.Total = context.Transactions.Sum(f => f.Amount);
                }
                else
                    pms = context.Transactions.OrderByDescending(f => f.When).ToList();

                return View("~/Views/Mentor/Accountancy/Index.cshtml",pms);
            }
        }

        /// <summary>
        /// Get view with payments
        /// </summary>
        /// <returns>View with all payments in the database</returns>
        public IActionResult Payments()
        {
            List<MPayment> payments = new List<MPayment>();
            using (var context = new OverstagContext())
            {
                var pms = context.Payments.OrderBy(f => f.PayedAt == null).OrderBy(f => f.PlacedAt).ToList();
                foreach (var pm in pms)
                {
                    try
                    {
                        var user = context.Accounts.First(f => f.Id == pm.UserID);
                        var invoice = context.Invoices.First(f => f.PayID == pm.InvoiceID);
                        payments.Add(new MPayment()
                        {
                            Invoice = invoice,
                            User = user,
                            Payment = pm
                        });
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return View("~/Views/Mentor/Accountancy/Payments.cshtml",payments);
        }

        [HttpPost]
        public IActionResult addTransaction(Accountancy.Transaction t)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    t.When = DateTime.Now;
                    context.Transactions.Add(t);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Interne fout", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Mark a payment as payed
        /// </summary>
        /// <param name="payed">0 or 1: not payed or payed</param>
        /// <param name="payid">The payment's id</param>
        /// <param name="invoiceid">The id of the invoice</param>
        /// <returns>JSON, status = success or status = error</returns>
        [HttpPost]
        public JsonResult markAsPayed([FromForm]int payed, [FromForm]int payid, [FromForm]int invoiceid)
        {
            using (var context = new OverstagContext())
            {
                try
                {
                    var invoice = context.Invoices.First(f => f.Id == invoiceid);
                    var payment = context.Payments.First(f => f.Id == payid);

                    if (payed == 0 || payed == 1)
                    {
                        DateTime? nd = null;
                        Mollie.Api.Models.Payment.PaymentStatus? np = null;

                        invoice.Payed = payed;
                        payment.PayedAt = (payed == 1) ? DateTime.Now : nd;
                        payment.PaymentID = (payed == 1) ? Encryption.Random.rCode(10) : null;
                        payment.Status = (payed == 1) ? Mollie.Api.Models.Payment.PaymentStatus.Paid : np;
                        context.Invoices.Update(invoice);
                        context.Payments.Update(payment);
                        context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[MENTOR] Betaling (#{payment.PaymentID}) van factuur door {context.Accounts.First(f => f.Id == invoice.UserID).Firstname}", When = DateTime.Now });
                        context.SaveChanges();
                        return Json(new { status = "success", payid = payment.PaymentID });
                    }
                    else
                        return Json(new { status = "error", error = "BOOL payed moet 0 of 1 zijn" });
                }
                catch (Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden", debuginfo = e.ToString() });
                }
            }
        }

        public IActionResult Invoices()
            => View("~/Views/Mentor/Accountancy/Invoices.cshtml", new OverstagContext().Invoices.Where(f => f.Payed == 0).OrderByDescending(g => g.Timestamp).ToList());

        /// <summary>
        /// Automatize invoicing for all users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult autoInvoice([FromForm]int amount)
        {
            int usercount = 0;
            int particount = 0;
            int moneycount = 0;
            int failedmails = 0;

            //ontvanger, mail
            List<Tuple<string, string>> Emails = new List<Tuple<string, string>>();

            using (var context = new OverstagContext())
            {
                var users = context.Accounts.Include(p => p.Subscriptions).Include(f => f.Family).ToList();

                try
                {
                    foreach (var user in users)
                    {
                        var parti = user.Subscriptions.Where(f => f.Payed == 0).ToList();

                        List<int> eventIDS = new List<int>();

                        int bill = 0;
                        int additions = 0;
                        int eventcount = 0;

                        foreach (var part in parti)
                        {
                            var eve = context.Events.First(f => f.Id == part.EventID);
                            if (Core.General.DateIsPassed(eve.When))
                            {
                                eventcount++;

                                bill += (eve.Cost * (part.FriendCount + 1));

                                additions += part.ConsumptionCount;
                                bill += part.ConsumptionTax;

                                for (int i = 0; i < part.FriendCount + 1; i++)
                                    eventIDS.Add(eve.Id);
                            }
                        }

                        var facture = new Invoice()
                        {
                            UserID = (user.Family == null) ? user.Id : user.Family.ParentID,
                            Amount = bill,
                            EventIDs = string.Join(',', eventIDS),
                            Payed = 0,
                            Timestamp = DateTime.Now,
                            PayID = Encryption.Random.rHash(user.Token),
                            AdditionsCount = additions
                        };

                        if (eventIDS.Count() > (amount-1))
                        {
                            context.Invoices.Add(facture);
                            parti.ForEach(f => f.Payed = 1);
                            user.Subscriptions = parti;
                            context.Accounts.Update(user);
                            context.SaveChanges();
                            moneycount += bill;
                            usercount++;
                            particount += eventcount;
                            Emails.Add(new Tuple<string, string>(user.Email,
                                $"<h1>Er is een factuur gemaakt</h1><h4>Beste {user.Firstname},<br>Er is automatisch een factuur gemaakt van de afgelopen avonden.<br>Deze kun je vinden onder <i>&quot;Betalingen&quot;</i> in je account op de website.<br>Hier is de link naar je factuur:<br><br><a href=\"https://stoverstag.nl/Pay/Direct/{Uri.EscapeDataString(facture.PayID)}\">https://stoverstag.nl/Pay/Direct/{Uri.EscapeDataString(facture.PayID)}</a><br></h4>"));
                        }
                    }

                    foreach (var email in Emails)
                    {
                        try
                        {
                            Core.General.SendMail("Factuur gemaakt", email.Item2, email.Item1);
                        }
                        catch
                        {
                            failedmails++;
                            continue;
                        }
                    }
                    return Json(new { status = "success", usercount, particount, moneycount, failedmails });
                }
                catch (Exception ex)
                {
                    return Json(new { status = "error", error = "Mislukt door interne fout", debuginfo = ex.ToString() });
                }
            }
        }

        /// <summary>
        /// Process unpayed events per family
        /// </summary>
        /// <returns>json with info</returns>
        public JsonResult processPUE()
        {
            //Zet ID's om van kind naar ouder
            using (var context = new OverstagContext())
            {
                int count = 0;
                int failed = 0;
                int succeed = 0;
                List<string> exceptions = new List<string>();

                foreach (var family in context.Families.Include(f => f.Members).ToList())
                {
                    if (family.Members.Count() <= 0)
                        continue;

                    var parent = context.Accounts.Include(f => f.Subscriptions).First(f => f.Id == family.ParentID);

                    List<Account> members = new List<Account>();

                    foreach (var user in family.Members)
                        members.Add(context.Accounts.Include(f => f.Subscriptions).First(f => f.Id == user.Id));

                    foreach (var m in members)
                        foreach (var f in m.Subscriptions.Where(g => g.Payed == 0))
                        {
                            parent.Subscriptions.Add(new Participate { UserID = parent.Id, EventID = f.EventID, FriendCount = f.FriendCount, ConsumptionTax = f.ConsumptionTax, ConsumptionCount = f.ConsumptionCount });
                            f.Payed = 1;
                            count++;
                        }

                    try
                    {
                        context.Accounts.Update(parent);
                        context.Accounts.UpdateRange(members);
                        context.SaveChanges();
                        succeed++;
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e.ToString());
                        failed++;
                    }
                }

                return Json(new { status = "success", count, succeed, failed, exceptions });
            }
        }
    }
}
