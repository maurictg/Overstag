using System;
using System.Collections.Generic;

namespace Overstag.Models.NoDB
{
    public struct ISubscription
    {
        public Event Event;
        public bool Subscribed;
        public int Friends;
    }

    public struct UserEvent
    {
        public Event Event;
        public List<SSub> Participators;
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

    public struct SSub
    {
        public Account account;
        public Participate part;
    }

    public struct SSubEvent
    {
        public Event Event;
        public List<SSub> Sub;
    }

    public struct FUnpayed
    {
        public Account User;
        public List<Event> UnpayedEvents;
        public int FriendCount;
        public int ConsumptionCount;
        public int ConsumptionCost;
    }

    public struct MPayment
    {
        public Account User;
        public Invoice Invoice;
        public Payment Payment;
    }
}
