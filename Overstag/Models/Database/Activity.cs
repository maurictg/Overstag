using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Overstag.Models.Database.Meta;
using Overstag.Models.Database.Relations;

namespace Overstag.Models.Database
{
    [Table("activity")]
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime When { get; set; }
        public int Cost { get; set; }
        public ActivityType Type { get; set; }

        [JsonIgnore]
        public List<Subscription> Subscriptions { get; set; }
    }
}
