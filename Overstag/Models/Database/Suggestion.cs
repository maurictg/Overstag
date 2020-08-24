using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Overstag.Models.Database
{
    public class Suggestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? EstimatedCost { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
