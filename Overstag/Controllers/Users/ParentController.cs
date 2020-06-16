using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Overstag.Authorization;
using Overstag.Models;

namespace Overstag.Controllers
{
    [OverstagAuthorize(1,2,3)]
    public class ParentController : OverstagController
    {
        /// <summary>
        /// Get parent index page with family info or create new family if not exist
        /// </summary>
        /// <returns>View with info</returns>
        public async Task <IActionResult> Index()
        {
            await using var context = new OverstagContext();
            if(!context.Families.Any()) await CreateFamily();
            return View(await context.Families.Include(f => f.Members).FirstAsync(f => f.ParentID == currentUser.Id));
        }

        /// <summary>
        /// Get unPaid events per user
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Billing()
        {
            await using var context = new OverstagContext();
            var family = await context.Families
                .Include(f => f.Members)
                .FirstAsync(i => i.ParentID == currentUser.Id);

            return View(family.Members);
        }

        /// <summary>
        /// Create a new family with currentuser as parent
        /// </summary>
        private async Task CreateFamily()
        {
            await using var context = new OverstagContext();
            try
            {
                await context.Families.AddAsync(new Family()
                {
                    ParentID = currentUser.Id,
                    Token = Encryption.Random.rHash(currentUser.Token)
                });
                await context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw e;
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
            try
            {
                await using var context = new OverstagContext();
                var family = await context.Families.Include(f => f.Members).FirstAsync(g => g.ParentID == currentUser.Id);
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

        /// <summary>
        /// Generate a invoice for all members of the family
        /// </summary>
        /// <returns>Json (status = success or status = error with details)</returns>
        public async Task<IActionResult> GenerateInvoice()
        {
            List<Exception> exceptions = new List<Exception>();
            await using var context = new OverstagContext();
            foreach (var member in (await context.Families.Include(f => f.Members).FirstAsync(g => g.ParentID == currentUser.Id)).Members)
            {
                bool result = await Services.Invoices.Create(member.Id);
                if (!result)
                    exceptions.Add(Services.Invoices.error);
            }

            if (exceptions.Any())
                return Json(new { status = "error", error = "Voor " + exceptions.Count() + " leden van de familie facturen mislukt te maken.", debuginfo = string.Join(',',exceptions.Select(x => x.Message)) });
            else
                return Json(new { status = "success" });
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
                await Services.Invoices.MergeFamilyInvoices(currentUser.Id);
                return Json(new { status = "success" });
            }
            catch(Exception e)
            {
                return Json(new { status = "error", error = "Het samenvoegen van de facturen is mislukt. Neem even contact met ons op", debuginfo = e.ToString() });
            }
        }
    }
}
