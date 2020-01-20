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
            try
            {
                await Services.Invoices.MergeFamilyInvoices(currentuser().Id);
                return Json(new { status = "success" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", debuginfo = e.ToString() });
            }
        }
    }
}
