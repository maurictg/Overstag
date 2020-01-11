using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using Microsoft.EntityFrameworkCore;

namespace Overstag.Services
{
    public class Invoices
    {
        public static Exception error;
        public static string invoiceId;
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

                    var subs = user.Subscriptions.Where(f => !f.Payed && Core.General.DateIsPassed(f.Event.When));

                    if (subs.Count() == 0)
                        return true;

                    foreach (var sub in subs)
                    {
                        additions += sub.AdditionsCost;
                        total += (sub.Event.Cost * (sub.FriendCount + 1));
                        sub.Payed = true;
                        eventIDS.Add(sub.EventID);
                    }

                    invoiceId = Encryption.Random.rHash(user.Token + DateTime.Now.ToLongTimeString());

                    await context.Invoices.AddAsync(new Invoice()
                    {
                        Amount = total + additions,
                        AdditionsCost = additions,
                        EventIDs = string.Join(',',eventIDS),
                        Payed = false,
                        Payment = null,
                        Timestamp = DateTime.Now,
                        User = user,
                        InvoiceID = invoiceId
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

        public static XInvoice GetXInvoice(int id)
        {
            var invoice = new OverstagContext().Invoices.Include(g => g.User).ThenInclude(h => h.Subscriptions).First(j => j.Id == id);
            invoice.User.Subscriptions = new OverstagContext().Accounts.Include(f => f.Subscriptions).ThenInclude(g => g.Event).First(j => j.Id == invoice.User.Id).Subscriptions;
            return GetXInvoice(invoice);
        }

        public static XInvoice GetXInvoice(Invoice i)
        {
            List<int> eventIDS = i.EventIDs.Split(',').Select(f => Convert.ToInt32(f)).ToList();
            Dictionary<Event, int> EventAndFriends = new Dictionary<Event, int>();

            foreach (int eventID in eventIDS)
            {
                var sub = i.User.Subscriptions.First(f => f.EventID == eventID);
                EventAndFriends.Add(sub.Event, sub.FriendCount);
            }

            return new XInvoice()
            {
                Payed = i.Payed,
                Amount = i.Amount,
                Events = EventAndFriends,
                Timestamp = i.Timestamp,
                User = i.User,
                InvoiceID = i.InvoiceID,
                AdditionsCost = i.AdditionsCost
            };
        }

    }
}
