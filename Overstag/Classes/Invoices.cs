using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Core
{
    public class Invoices
    {
        public static Exception error;
        public static async Task<bool> Create(int userId)
        {
            try
            {
                using(var context = new OverstagContext())
                {
                    var user = await context.Accounts.Include(f => f.Subscriptions).ThenInclude(f => f.Event).Include(g => g.Invoices).FirstOrDefaultAsync(h => h.Id == userId);
                    if(user == null)
                        throw new ArgumentException("User not found");

                    int total = 0;
                    int additions = 0;
                    List<int> eventIDS = new List<int>();

                    var subs = user.Subscriptions.Where(f => !f.Payed);

                    if (subs.Count() == 0)
                        return true;

                    foreach (var sub in subs)
                    {
                        additions += sub.AdditionsCost;
                        total += (sub.Event.Cost * (sub.FriendCount + 1));
                        sub.Payed = true;
                        eventIDS.Add(sub.EventID);
                    }

                    
                    await context.Invoices.AddAsync(new Invoice()
                    {
                        Amount = total + additions,
                        AdditionsCost = additions,
                        EventIDs = string.Join(',',eventIDS),
                        Payed = false,
                        Payment = null,
                        Timestamp = DateTime.Now,
                        User = user,
                        InvoiceID = Encryption.Random.rHash(user.Token+DateTime.Now.ToLongTimeString())
                    });

                    context.Accounts.Update(user);

                    await context.SaveChangesAsync();
                }
                return true;
            }
            catch(Exception e)
            {
                error = e;
                return false;
            }
        }
    }
}
