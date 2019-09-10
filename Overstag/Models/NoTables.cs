using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Overstag.Models;

namespace Overstag.Models.NoDB
{
    public struct Subscriptions
    {
        public List<Event> Events { get; set; }
    }

    public struct UserEvent
    {
        public Event Event;
        public List<Account> Participators;
    }

    public struct UnpayedEvents
    {
        public List<IInvoice> Invoices;
        public List<Event> UnfacturedEvents;
        public List<Participate> Subscriptions;
    }

    public struct IInvoice
    {
        public int UserID;
        public int Amount;
        public List<Event> Events;
        public bool Payed;
        public DateTime Timestamp;
        public string PayID;
        public int Additions;
    }
    
    public struct AUnpayed
    {
        public Account User;
        public List<Invoice> Unpayed_Invoices;
        public List<Event> Unfactured_Events;
    }

    public struct AParticipator
    {
        public Event Event;
        public Account[] Accounts;
        public bool[] Factured;
    }

    public struct OPayInfo
    {
        public Account User;
        public IInvoice Invoice;
    }

    public struct FUnpayed
    {
        public Account User;
        public List<Event> UnpayedEvents;
        public int ConsumptionCount;
        public int ConsumptionCost;
    }
}
