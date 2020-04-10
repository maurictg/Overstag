#define MOLLIE_ENABLED

//COMMENT this to enable mollie
#undef MOLLIE_ENABLED
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Overstag.Models;
using Overstag.Models.NoDB;
using Overstag.Accountancy;
using Microsoft.EntityFrameworkCore;
using Mollie.Api.Client;
using Mollie.Api.Models.Payment.Response;
using Mollie.Api.Models.Payment;
using Microsoft.AspNetCore.Http;

namespace Overstag.Controllers
{
    public class AccountancyController : OverstagController
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
                int[] typesOut = { 21, 22, 23, 24, 25, 26, 27, 28, 29 };

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
                    Balance = trans.Where(g => g.Payed).Sum(f => f.Amount),
                    BalanceWithDC = trans.Sum(f => f.Amount),
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
                                    var invoice = context.Invoices.Find(payment.Invoice.Id);
                                    if (invoice != null)
                                    {
                                        invoice.Payed = true;
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
                var pms = context.Payments.Include(x => x.User).Include(y => y.Invoice).OrderBy(f => f.PayedAt == null).OrderByDescending(f => f.PlacedAt).ToList();
                foreach (var pm in pms)
                {
                    try
                    {
                        var user = pm.User;
                        var invoice = pm.Invoice;
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

        [Route("Accountancy/Declaration/{id}")]
        public IActionResult Declaration(int id)
        {
            using(var context = new OverstagContext())
            {
                var declaration = context.Transactions.Include(f => f.User).FirstOrDefault(f => f.Id == id);
                string[] error = { "Declaratie niet gevonden", "Probeer een andere." };
                if (declaration != null)
                    return View("~/Views/Mentor/Accountancy/Declaration.cshtml",declaration);
                else
                    return View("~/Views/Error/Custom.cshtml", error);
            }
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
                    t.Timestamp = DateTime.Now;
                    t.UserId = currentUser.Id;
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
                    var t = await context.Transactions.FirstAsync(f => f.Id == id);
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


        [HttpPost]
        public async Task<IActionResult> setTransactionAsPayed([FromForm]int id)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var t = await context.Transactions.FirstAsync(f => f.Id == id);
                    t.Payed = true;
                    context.Transactions.Update(t);
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
                    var invoice = context.Invoices.Include(x => x.User).First(f => f.Id == invoiceid);
                    var payment = context.Payments.First(f => f.Id == payid);

                    if (payed == 0 || payed == 1)
                    {
                        DateTime? nd = null;
                        Mollie.Api.Models.Payment.PaymentStatus? np = null;

                        invoice.Payed = (payed == 1);
                        payment.PayedAt = (payed == 1) ? DateTime.Now : nd;
                        payment.PaymentID = (payed == 1) ? Encryption.Random.rCode(10) : null;
                        payment.Status = (payed == 1) ? Mollie.Api.Models.Payment.PaymentStatus.Paid : np;
                        context.Invoices.Update(invoice);
                        context.Payments.Update(payment);
                        context.Transactions.Add(new Accountancy.Transaction { Amount = invoice.Amount, Description = $"[MENTOR] Betaling (#{payment.PaymentID}) van factuur door {invoice.User.Firstname}", When = DateTime.Now, Type = 1, Payed = true, UserId = currentUser.Id });
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
            => View("~/Views/Mentor/Accountancy/Invoices.cshtml", new OverstagContext().Invoices.Where(f => !f.Payed).OrderByDescending(g => g.Timestamp).Include(h => h.User).ToList());

        /// <summary>
        /// Automatize invoicing for all users
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> autoInvoice([FromForm]int amount, [FromForm]bool sendmail)
        {
            int usercount = 0;
            int failedmails = 0;
            List<string> errors = new List<string>();

            //ontvanger, token
            List<(Account, string)> usersToEmail = new List<(Account, string)>();

            using (var context = new OverstagContext())
            {
                var users = context.Accounts.Include(p => p.Subscriptions).Include(f => f.Family).ToList();

                foreach (var user in users)
                {
                    if(user.Subscriptions.Count(f => !f.Payed) >= amount && user.Family == null)
                    {
                        bool result = await Services.Invoices.Create(user.Id);
                        if (!result)
                            errors.Add(Services.Invoices.error.ToString());

                        usercount++;
                        usersToEmail.Add((user,Services.Invoices.invoiceId));
                    }
                }

                if (sendmail)
                {
                    foreach (var email in usersToEmail)
                    {
                        if (!new Services.InvoiceEmail(email.Item1, email.Item2).SendAsync().Result)
                            failedmails++;
                    }
                }

                return Json(new { status = "success", usercount, failedmails });
            }
        }

        /// <summary>
        /// Process unpayed events per family
        /// </summary>
        /// <returns>json with info</returns>
        public async Task<JsonResult> processPUE()
        {
            using (var context = new OverstagContext())
            {
                int count = 0;
                int failed = 0;
                int succeed = 0;
                List<string> exceptions = new List<string>();

                foreach (var family in context.Families.Include(f => f.Members).ToList())
                {
                    if (family.Members.Count() == 0)
                        continue;

                    foreach (var member in family.Members)
                    {
                        count++;
                        bool result = await Services.Invoices.Create(member.Id);
                        if (!result)
                            failed++;
                        else
                            succeed++;
                    }

                    try
                    {
                        await Services.Invoices.MergeFamilyInvoices(family.ParentID);
                    }
                    catch(Exception e)
                    {
                        Services.Invoices.error = e;
                        failed++;
                    }
                }

                return Json(new { status = "success", count, succeed, failed, exceptions });
            }
        }
    }
}
