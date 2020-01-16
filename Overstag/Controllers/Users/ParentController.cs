using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Overstag.Models;

namespace Overstag.Controllers
{
    public class ParentController : Controller
    {
        /// <summary>
        /// Gets the current user
        /// </summary>
        /// <returns>Overstag Account model</returns>
        public Account currentuser() 
            => new OverstagContext().Accounts.First(f => f.Token == HttpContext.Session.GetString("Token"));

        /// <summary>
        /// Get parent index page with family info or create new family if not exist
        /// </summary>
        /// <returns>View with info</returns>
        public IActionResult Index()
        {
            using(var context = new OverstagContext())
            {
                bool has = (context.Families.FirstOrDefault(f => (f.ParentID == currentuser().Id))!=null);

                if(!has)
                    CreateFamily();

                return View(context.Families.Include(f => f.Members).First(f => f.ParentID == currentuser().Id));
            }
        }

        /// <summary>
        /// Get unpayed events per user
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Billing()
        {
            using(var context = new OverstagContext())
            {
                var family = context.Families
                    .Include(f => f.Members).ThenInclude(g => g.Subscriptions).ThenInclude(h => h.Event)
                    //.Include(f => f.Members).ThenInclude(g => g.Invoices) <-- This is how you also include invoices
                    .First(i => i.ParentID == currentuser().Id);

                return View(family.Members);
            }
        }

        /// <summary>
        /// Create a new family with currentuser as parent
        /// </summary>
        private void CreateFamily()
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    context.Families.Add(new Family()
                    {
                        ParentID = currentuser().Id,
                        Token = Encryption.Random.rHash(currentuser().Token)
                    });
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    throw e;
                }
            }
            
        }

        /// <summary>
        /// Removes user from family
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Parent/Remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var family = context.Families.Include(f => f.Members).First(g => g.ParentID == currentuser().Id);
                    family.Members.Remove(context.Accounts.First(f => f.Id == id));
                    context.Families.Update(family);
                    await context.SaveChangesAsync();
                    return Json(new { status = "success" });
                }
                catch(Exception e)
                {
                    return Json(new { status = "error", error = "Verwijderen is mislukt", debuginfo = e });
                }
            }
        }

        /// <summary>
        /// Generate a invoice for all members of the family
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        public async Task<IActionResult> GenerateInvoice()
        {
            List<Exception> exceptions = new List<Exception>();
            using(var context = new OverstagContext())
            {
                foreach (var member in context.Families.Include(f => f.Members).First(g => g.ParentID == currentuser().Id).Members)
                {
                    bool result = await Services.Invoices.Create(member.Id);
                    if (!result)
                        exceptions.Add(Services.Invoices.error);
                }

                if (exceptions.Count() > 0)
                    return Json(new { status = "error", error = "Voor " + exceptions.Count() + " leden van de familie facturen mislukt te maken." });
                else
                    return Json(new { status = "success" });
            }
        }

        /*
         * 1. Get all participations
         * 2. Make Event unique in participations
         * 3. Set participations on parent's name
         * 4. Create new invoice with events
         * 
         */
        public async Task<IActionResult> MergeInvoices()
        {
            using (var context = new OverstagContext())
            {
                var cuser = context.Accounts
                    .Include(g => g.Subscriptions).ThenInclude(i => i.Event)
                    .Include(h => h.Invoices).First(f => f.Id == currentuser().Id);

                var family = context.Families
                    .Include(g => g.Members).ThenInclude(h => h.Subscriptions).ThenInclude(i => i.Event)
                    .Include(g => g.Members).ThenInclude(h => h.Invoices)
                    .First(f => f.ParentID == cuser.Id);

                // event, (friendCount, consumptionTax)
                Dictionary<Event, (int, int)> Subs = new Dictionary<Event, (int, int)>(); 

                
                foreach (var member in family.Members)
                {
                    if (member.Invoices.Count(f => !f.Payed) == 0)
                        continue;

                    foreach (var invoice in member.Invoices.Where(f => !f.Payed))
                    {
                        List<Participate> participates = new List<Participate>();

                        foreach (int eventID in invoice.EventIDs.Split(',').Select(f => Convert.ToInt32(f)).ToList())
                            participates.Add(member.Subscriptions.First(f => f.EventID == eventID));

                        foreach (var part in participates)
                        {
                            if (Subs.ContainsKey(part.Event))
                            {
                                ValueTuple<int, int> values;
                                Subs.TryGetValue(part.Event, out values);

                                values.Item1 += (part.FriendCount + 1);
                                values.Item2 += part.AdditionsCost;
                                Subs[part.Event] = values;
                            }
                            else
                                Subs.Add(part.Event, (part.FriendCount, part.AdditionsCost));
                        }

                        context.Invoices.Remove(invoice);
                    }
                }

                foreach (var sub in Subs)
                    cuser.Subscriptions.Add(new Participate { Event = sub.Key, FriendCount = (byte)sub.Value.Item1, AdditionsCost = sub.Value.Item2, Payed = true });

                await context.Invoices.AddAsync(new Invoice() { 
                    AdditionsCost = Subs.Values.Sum(f => f.Item2), 
                    EventIDs = string.Join(',', Subs.Select(f => f.Key.Id)), 
                    User = cuser,
                    Payed = false,
                    Timestamp = DateTime.Now,
                    InvoiceID = Encryption.Random.rHash("INV"+DateTime.Now.ToLongTimeString()),
                    Amount = (Subs.Sum(f => (f.Key.Cost * (1 + f.Value.Item1))) + Subs.Sum(f => f.Value.Item2))
                });

                context.Accounts.Update(cuser);
                await context.SaveChangesAsync();
                return Json(new { status = "success" });
            }
        }
    }
}
