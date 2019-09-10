using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Overstag.Models;
using Overstag.Models.NoDB;

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
        [Route("Parent/Family")]
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
            List<FUnpayed> Billing = new List<FUnpayed>();
            using(var context = new OverstagContext())
            {
                List<Account> Users = new List<Account>();

                foreach (var user in context.Families.Include(f => f.Members).First(g => g.ParentID == currentuser().Id).Members)
                    Users.Add(context.Accounts.Include(f => f.Subscriptions).First(g => g.Id == user.Id));

                foreach(var user in Users)
                {
                    int ccnt = 0;
                    int cc = 0;

                    List<Event> Events = new List<Event>();
                    foreach (var s in user.Subscriptions.Where(s => s.Payed == 0))
                    {
                        ccnt += s.ConsumptionCount;
                        cc += s.ConsumptionTax;
                        var e = context.Events.First(f => f.Id == s.EventID);
                        if(Core.General.DateIsPassed(e.When))
                            Events.Add(e);
                    }
                        
                    Billing.Add(new FUnpayed { UnpayedEvents = Events.OrderBy(b => b.When).ToList(), User = user, ConsumptionCount = ccnt, ConsumptionCost = cc });
                }
            }

            return View(Billing);
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
                    context.Add(new Family()
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
        public IActionResult Remove(int id)
        {
            using(var context = new OverstagContext())
            {
                try
                {
                    var family = context.Families.Include(f => f.Members).First(g => g.ParentID == currentuser().Id);
                    family.Members.Remove(context.Accounts.First(f => f.Id == id));
                    context.Families.Update(family);
                    context.SaveChanges();
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
        public IActionResult GenerateInvoice()
        {
            try
            {
                using (var context = new OverstagContext())
                {
                    var family = context.Families.Include(f => f.Members).First(g => g.ParentID == currentuser().Id);
                    List<Account> Members = new List<Account>();

                    //Get accounts
                    foreach (var user in family.Members)
                        Members.Add(context.Accounts.Include(f => f.Subscriptions).First(f => f.Id == user.Id));

                    List<int> EventIDS = new List<int>();
                    int bill = 0;
                    int ccnt = 0;

                    //Get events that are unfactured per user
                    foreach (var member in Members)
                    {
                        foreach (var sub in member.Subscriptions.Where(p => p.Payed == 0))
                        {
                            var eve = context.Events.First(f => f.Id == sub.EventID);
                            if (Core.General.DateIsPassed(eve.When))
                            {
                                sub.Payed = 1;
                                bill += sub.ConsumptionTax;
                                bill += eve.Cost;
                                ccnt += sub.ConsumptionCount;
                                EventIDS.Add(eve.Id);
                            }
                        }

                        context.Accounts.Update(member);
                    }


                    var facture = new Invoice()
                    {
                        UserID = currentuser().Id,
                        Amount = bill,
                        EventIDs = string.Join(',', EventIDS),
                        Payed = 0,
                        Timestamp = DateTime.Now,
                        PayID = Encryption.Random.rHash(currentuser().Token),
                        AdditionsCount = ccnt
                    };

                    context.Invoices.Add(facture);
                    context.SaveChanges();
                    return Json(new { status = "success" });
                }
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Mislukt door interne fout", debuginfo = e.Message });
            }
        }

    }
}
