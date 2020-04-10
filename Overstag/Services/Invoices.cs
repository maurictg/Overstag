using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Overstag.Services
{
    public class Invoices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Invoices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public static Exception error;
        public static string invoiceId;
        public static async Task<bool> Create(int userId)
        {
            try
            {
                using(var context = new OverstagContext())
                {
                    var user = await context.Accounts.Include(f => f.Subscriptions).ThenInclude(f => f.Event)
                        .Include(g => g.Invoices).FirstOrDefaultAsync(h => h.Id == userId);

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
            var invoice = new OverstagContext().Invoices
                .Include(f => f.Payment)
                .Include(g => g.User).ThenInclude(h => h.Subscriptions).ThenInclude(i => i.Event)
                .First(j => j.Id == id);

            return GetXInvoice(invoice);
        }

        /// <summary>
        /// Get's XInvoice. Needs invoice with included User with incluced Subscriptions and included Events
        /// </summary>
        /// <param name="i">Invoice with included Event</param>
        /// <returns>XInvoice instance</returns>
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
                Id = i.Id,
                EventIDs = i.EventIDs,
                Payment = i.Payment,
                PaymentID = i.PaymentID,
                Payed = i.Payed,
                Amount = i.Amount,
                Events = EventAndFriends,
                Timestamp = i.Timestamp,
                User = i.User,
                InvoiceID = i.InvoiceID,
                AdditionsCost = i.AdditionsCost
            };
        }

        public static async Task<bool> MergeFamilyInvoices(int parentID, bool sendmail = false)
        {
            using (var context = new OverstagContext())
            {
                var parent = context.Accounts
                    .Include(g => g.Subscriptions).ThenInclude(i => i.Event)
                    .Include(h => h.Invoices).First(f => f.Id == parentID);

                var family = context.Families
                    .Include(g => g.Members).ThenInclude(h => h.Subscriptions).ThenInclude(i => i.Event)
                    .Include(g => g.Members).ThenInclude(h => h.Invoices)
                    .First(f => f.ParentID == parent.Id);

                if (family.Members.Count() == 0)
                    return true;

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

                if (Subs.Count() == 0)
                    return true;

                List<Participate> existingSubscriptions = parent.Subscriptions.ToList();
                
                foreach (var sub in Subs)
                {
                    if(!existingSubscriptions.Any(f => f.EventID == sub.Key.Id))
                    {
                        parent.Subscriptions.Add(new Participate { Event = sub.Key, FriendCount = (byte)sub.Value.Item1, AdditionsCost = sub.Value.Item2, Payed = true });
                    }
                }

                await context.Invoices.AddAsync(new Invoice()
                {
                    AdditionsCost = Subs.Values.Sum(f => f.Item2),
                    EventIDs = string.Join(',', Subs.Select(f => f.Key.Id)),
                    User = parent,
                    Payed = false,
                    Timestamp = DateTime.Now,
                    InvoiceID = Encryption.Random.rHash("INV" + DateTime.Now.ToLongTimeString()),
                    Amount = (Subs.Sum(f => (f.Key.Cost * (1 + f.Value.Item1))) + Subs.Sum(f => f.Value.Item2))
                });

                context.Accounts.Update(parent);
                await context.SaveChangesAsync();

                

                return true;
            }
        }

    }
}
