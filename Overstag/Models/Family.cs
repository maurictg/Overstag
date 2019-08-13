using System;

namespace Overstag.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string UserID { get; set; } //parent
        public string Token { get; set; }
    }

    public class FamilyInvoice
    {
        public int Id { get; set; }
        public int FamilyID { get; set; }
        public int InvoiceID { get; set; } //id van invoice die wordt gemaakt
        public int ParticipatorCount { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
