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

    public struct UnpayedEvents
    {
        public List<IInvoice> Invoices;
        public List<Event> UnfacturedEvents;
    }

    public struct IInvoice
    {
        public int UserID;
        public int Amount;
        public List<Event> Events;
        public bool Payed;
        public DateTime Timestamp;
        public string PayID;
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
}
