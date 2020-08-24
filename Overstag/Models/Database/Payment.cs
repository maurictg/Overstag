using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Overstag.Models.Database.Meta;

namespace Overstag.Models.Database
{
    [Table("payment")]
    public class Payment
    {
        public int Id { get; set; }
        public PaymentType Type { get; set; }
        public PaymentStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public string Metadata { get; set; }

        [JsonIgnore]
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }

        //Getters
        public bool Paid { get { return Status == PaymentStatus.PAID && PaidAt != null;  } }
    }
}
