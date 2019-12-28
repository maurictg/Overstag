﻿#define MOLLIE_ENABLED

//COMMENT this to enable mollie
#undef MOLLIE_ENABLED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using Overstag.Models.NoDB;
using Microsoft.EntityFrameworkCore;
using Mollie.Api.Client;
using Mollie.Api.Models.Payment.Response;
using Mollie.Api.Models.Payment;

namespace Overstag.Controllers
{
    public class AccountancyController : Controller
    {
        /// <summary>
        /// Get a range of transactions
        /// </summary>
        /// <param name="amount">A range. Default 500</param>
        /// <returns>View with transactions</returns>
        [Route("Accountancy/Index/{amount?}")]
        public IActionResult Index(int? amount)
        {
            using (var context = new OverstagContext())
            {
                var trans = context.Transactions.OrderByDescending(f => f.When).ToList();
                
                int[] typesIn = { 1, 2, 3, 4, 5, 6 };
                int[] typesOut = { 21, 22, 23, 24, 25, 26, 27, 28 };

                Dictionary<int, int> OPT = new Dictionary<int, int>();
                Dictionary<int, int> IPT = new Dictionary<int, int>();
                typesIn.ToList().ForEach(f => IPT.Add(f, trans.Where(g => g.Type == f).Sum(h => h.Amount)));
                typesOut.ToList().ForEach(f => OPT.Add(f, trans.Where(g => g.Type == f).Sum(h => h.Amount)));

                List<_Transaction> ts = new List<_Transaction>();
                int balance = 0;

                var results = trans.ToArray().Reverse().GroupBy(a => a.When.Date, b => b.Amount,
                    (c, d) => new { When = c, Amount = d.Sum() });

                foreach (var item in results)
                {
                    balance += item.Amount;

                    ts.Add(new _Transaction()
                    {
                        When = item.When.ToString("dd-MM-yyyy"),
                        Amount = Math.Round((double)balance / 100, 2).ToString("F").Replace(",", ".")
                    });
                }

                return View("~/Views/Mentor/Accountancy/Index.cshtml", new _Transactions()
                {
                    Balance = trans.Sum(f => f.Amount),
                    In = trans.Where(f => f.Amount > 0).Sum(g => g.Amount),
                    Out = trans.Where(f => f.Amount < 0).Sum(g => g.Amount),
                    OutPerType = OPT,
                    InPerType = IPT,
                    Transactions_ = ts,
                    Transactions = trans.Take((amount != null) ? Convert.ToInt32(amount) : trans.Count()).ToList(),
                    Limit = (amount != null) ? Convert.ToInt32(amount) : -1
                });
            }
        }

        /// <summary>
        /// Sync payments with mollie server
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UpdatePayments()
        {
#if !MOLLIE_ENABLED
            return Json(new { status = "error", error = "IDeal is uitgeschakeld. Kan betalingen niet verversen" });
#endif

            using(var context = new OverstagContext())
            {
                try
                {
                    foreach (var payment in context.Payments)
                    {
                        if (payment.Status != null)
                        {
                            if (new List<int>() { 0, 2, 3}.Contains((int)payment.Status))
                            {
                                PaymentClient pc = new PaymentClient(Core.General.Credentials.mollieApiToken);
                                PaymentResponse ps = await pc.GetPaymentAsync(payment.PaymentID);

                                payment.PayedAt = ps.PaidAt;
                                payment.Status = ps.Status;

                                if ((int)payment.Status == 6)
                                {
                                    var invoice = context.Invoices.FirstOrDefault(f => f.PayID == payment.InvoiceID);
                                    if (invoice != null)
                                    {
                                        invoice.Payed = 1;
                                        context.Invoices.Update(invoice);
                                        await context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }

                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is een interne fout opgetreden.", debuginfo = e.ToString() });
                }
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
                var pms = context.Payments.OrderBy(f => f.PayedAt == null).OrderByDescending(f => f.PlacedAt).ToList();
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

        /// <summary>
        /// Add transaction to database
        /// </summary>
        /// <param name="t">Transaction object</param>
        /// <returns>JSON, status=success or error</returns>
        [HttpPost]
        public async Task<IActionResult> addTransaction(Accountancy.Transaction t)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    t.When = (t.When < DateTime.Now.AddYears(-25)) ? DateTime.Now : t.When;
                    context.Transactions.Add(t);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Interne fout", debuginfo = e.ToString() });
            }
        }

        /// <summary>
        /// Remove transaction by its ID
        /// </summary>
        /// <param name="id">The transaction's id</param>
        /// <returns>JSON, status=success <or error/returns>
        [HttpPost]
        public async Task<IActionResult> removeTransaction([FromForm]int id)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var t = context.Transactions.First(f => f.Id == id);
                    context.Transactions.Remove(t);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
            }
            catch (Exception e)
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
        public async Task<JsonResult> markAsPayed([FromForm]int payed, [FromForm]int payid, [FromForm]int invoiceid)
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
                        context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[MENTOR] Betaling (#{payment.PaymentID}) van factuur door {context.Accounts.First(f => f.Id == invoice.UserID).Firstname}", When = DateTime.Now, Type = 1 });
                        await context.SaveChangesAsync();
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

        /// <summary>
        /// Removes payment by it's id
        /// </summary>
        /// <param name="payid">The payment its id</param>
        /// <returns>Json, status = success or error</returns>
        public async Task<IActionResult> removePayment([FromForm]int payid)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var payment = context.Payments.First(f => f.Id == payid);
                    context.Payments.Remove(payment);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Er is iets fout gegaan", debuginfo = e.Message });
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
        public async Task<JsonResult> autoInvoice([FromForm]int amount)
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
                        var parti = user.Subscriptions.ToList();

                        List<int> eventIDS = new List<int>();

                        int bill = 0;
                        int additions = 0;
                        int eventcount = 0;

                        foreach (var part in parti.Where(f => f.Payed == 0))
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
                            await context.SaveChangesAsync();
                            moneycount += bill;
                            usercount++;
                            particount += eventcount;
                            Emails.Add(new Tuple<string, string>(user.Email,
                                $"<h1>Er is een factuur gemaakt</h1><h4>Beste {user.Firstname},<br>Er is automatisch een factuur gemaakt van de afgelopen avonden.<br>Deze kun je vinden onder <i>&quot;Betalingen&quot;</i> in je account op de website.<br>Hier is de link naar je factuur:<br><br><a href=\"https://stoverstag.nl/Pay/Invoice/{Uri.EscapeDataString(facture.PayID)}\">https://stoverstag.nl/Pay/Invoice/{Uri.EscapeDataString(facture.PayID)}</a><br></h4>"));
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
        public async Task<JsonResult> processPUE()
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
                        await context.SaveChangesAsync();
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