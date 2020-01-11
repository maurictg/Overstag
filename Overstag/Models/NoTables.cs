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
        public List<XInvoice> Invoices;
        public List<Event> UnfacturedEvents;
        public List<Participate> Subscriptions;
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

    public struct _Transaction
    {
        public string When;
        public string Amount;
    }

    public struct _Transactions
    {
        public List<_Transaction> Transactions_;
        public List<Accountancy.Transaction> Transactions;
        public Dictionary<int, int> OutPerType;
        public Dictionary<int, int> InPerType;
        public double Balance;
        public double Out;
        public double In;
        public int Limit;
    }

    
}
