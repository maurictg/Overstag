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
                    Balance = trans.Where(g => g.Paid).Sum(f => f.Amount),
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
        /// View declaration details from user
        /// </summary>
        /// <param name="id">The id</param>
        /// <returns>A view with info</returns>
        [Route("Accountancy/Declaration/{id}")]
        public IActionResult Declaration(int id)
        {
            using (var context = new OverstagContext())
            {
                var declaration = context.Transactions.Include(f => f.User).FirstOrDefault(f => f.Id == id);
                string[] error = { "Declaratie niet gevonden", "Probeer een andere." };
                if (declaration != null)
                    return View("~/Views/Mentor/Accountancy/Declaration.cshtml", declaration);
                else
                    return View("~/Views/Error/Custom.cshtml", error);
            }
        }

        /// <summary>
        /// List all payments
        /// </summary>
        /// <returns>View with list<payment></payment></returns>
        public async Task<IActionResult> Payments()
        {
            var payments = await new OverstagContext().Payments.Include(a => a.User).Include(b => b.Invoice).OrderByDescending(f => f.PlacedAt).ToListAsync();
            return View("~/Views/Mentor/Accountancy/Payments.cshtml", payments);
        }

        /// <summary>
        /// Set the status of a payment
        /// </summary>
        /// <param name="id">The payment's id</param>
        /// <param name="action">The action to do  with the payment</param>
        /// <returns>JSON</returns>
        [HttpPost]
        public async Task<IActionResult> SetStatus([FromForm]int id, [FromForm]int action)
        {
            PayType[] allowedTypes = { PayType.BANK };
            using(var context = new OverstagContext())
            {
                var payment = await context.Payments.Include(a => a.Invoice).Include(b => b.User).FirstOrDefaultAsync(f => f.Id == id);

                if (payment == null)
                    return Json(new { status = "error", error = "Betaling niet gevonden." });

                if(action == 4) //Update payment
                    return await new PayController().UpdatePayment(payment.PaymentId);

                if (!allowedTypes.Contains(payment.PayType))
                    return Json(new { status = "error", error = "Dit type betaling kan niet worden aangepast." });

                if (payment.IsPaid())
                    return Json(new { status = "error", error = "Deze betaling is al afgerond, en kan daarom niet meer worden aangepast" });

                switch (action)
                {
                    case 0: //Mark as Paid
                        {
                            payment.Status = Mollie.Api.Models.Payment.PaymentStatus.Paid;
                            payment.PaymentId = "Betaalverzoek";
                            payment.PaidAt = DateTime.Now;
                            payment.Invoice.Paid = true;
                            context.Transactions.Add(new Transaction() { UserId = currentUser.Id, Paid = false, Timestamp = DateTime.Now, When = DateTime.Now.Date, Type = 1, Amount = payment.Invoice.Amount, Description = $"[BANK] Betaling van factuur #{payment.Invoice.Id} door {payment.User.Firstname} {payment.User.Lastname}" });
                        }
                        break;
                    case 1: //Cancel
                        payment.Status = Mollie.Api.Models.Payment.PaymentStatus.Canceled;
                        break;
                    case 2: //Delete
                        payment.Invoice = null;
                        payment.User = null;
                        context.Payments.Remove(payment);
                        break;
                    default:
                        return Json(new { status = "error", error = "Deze actie wordt niet ondersteund" });
                }

                if(action != 2)
                    context.Payments.Update(payment);

                await context.SaveChangesAsync();
                return Json(new { status = "success" });
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

        /// <summary>
        /// Set a transaction as payed
        /// </summary>
        /// <param name="id">The transaction's id</param>
        /// <returns>JSON</returns>
        [HttpPost]
        public async Task<IActionResult> setTransactionAsPaid([FromForm]int id)
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var t = await context.Transactions.FirstAsync(f => f.Id == id);
                    t.Paid = true;
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

        public IActionResult Invoices()
            => View("~/Views/Mentor/Accountancy/Invoices.cshtml", new OverstagContext().Invoices.Where(f => !f.Paid).OrderByDescending(g => g.Timestamp).Include(h => h.User).ToList());

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
                    if(user.Subscriptions.Count(f => !f.Paid) >= amount && user.Family == null)
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
        /// Process unPaid events per family
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
