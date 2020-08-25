using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Overstag.Models.Database
{
    [Table("suggestion")]
    public class Suggestion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? EstimatedCost { get; set; }

        [JsonIgnore]
        public User User { get; set; }
        public int UserId { get; set; }

        [JsonIgnore]
        public List<Relations.Vote> Votes { get; set; }
    }
}
